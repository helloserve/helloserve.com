using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using helloserve.Web;
using helloserve.Common;

namespace helloserve.Web
{
    public class AccountController : BaseController
    {

        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            return View();
        }

        public ActionResult LogOnPartial()
        {
            AuthenticationModel model = new AuthenticationModel();
            if (Request.IsAuthenticated)
            {
                model.Authenticated = true;
                Settings.Current.User = UserRepo.ValidateUser(User.Identity.Name, true);
            }

            return PartialView("_LogOnPartial", model);
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User user = UserRepo.ValidateUser(model.UserName, model.Password);
                if (user != null)
                {
                    LogRepo.LogForUser(user.UserID, "Logon", "Account.LogOn");

                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    Settings.Current.User = user;

                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The Username or Password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult ForgotPassword(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                ModelState.AddModelError("UserName", "Please provide a UserName");
                return PartialView("_LogOn");
            }

            User user = UserRepo.ValidateUser(username, true);

            try
            {
                if (user != null)
                {
                    string newPassword = user.ResetPassword();
                    Email.SendResetConfirmation(user, newPassword);
                }
            }
            catch (Exception ex)
            {
                ErrorRepo.LogException(ex);
                ModelState.AddModelError("ResetEmail", "We got a problem sending you your reset password email. Please try again in a while.");
            }
                       
            return PartialView("ResetPassword", user);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            int? userID = Settings.Current.GetUserID();
            if (userID.HasValue)
                LogRepo.LogForUser(userID.Value, "Logoff", "Account.LogOff");
            else
                ErrorRepo.LogUnknownError("Could not log off user??");

            FormsAuthentication.SignOut();
            Settings.Current.User = null;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult CheckUsername(string username)
        {
            User user = UserRepo.ValidateUser(username, false);

            if (user == null)
                return ReturnJsonResult(false, "Valid");
            else
                return ReturnJsonResult(true, "Invalid");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user

                User user = UserRepo.RegisterUser(model.UserName, model.Password, model.Email, model.ReceiveUpdates);
                try
                {
                    if (user != null)
                    {
                        LogRepo.LogForUser(user.UserID, "Registered", "Account.Register");
                        //let's send an email
                        Email.SendActivation(user);
                        return View("ConfirmRegister");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Could not register user");
                    }
                }
                catch (Exception ex)
                {
                    while (ex.InnerException != null)
                        ex = ex.InnerException;

                    ErrorRepo.LogException(ex);

                    ModelState.AddModelError("", "My bad... something gone wrong...");
                }
            }

            // If we got this far, something failed, redisplay form
            return View("Register", model);
        }

        [HttpGet]
        public ActionResult Activate(string id)
        {
            try
            {
                Guid guid = Guid.Parse(id);

                User user = UserRepo.ActivateUser(guid);

                if (user != null)
                {
                    LogRepo.LogForUser(user.UserID, "Activated", "Account.Activate");

                    user.Activated = true;
                    user.Save();

                    FormsAuthentication.SetAuthCookie(user.Username, false);
                    Settings.Current.User = user;

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ErrorRepo.LogControllerError(string.Format("ID {0} does not match any account", id), "Account.Activate");
                    throw new Exception("No account found to activate.");
                }
            }
            catch (Exception ex)
            {
                ErrorRepo.LogException(ex);
                return View("ActivationError", ex);
            }
        }

        public ActionResult Profile()
        {
            return View("Profile", new ProfileModel());
        }

        [Authorize]
        [HttpPost]
        public ActionResult UpdateProfile(FormCollection form)
        {
            ProfileModel model = new ProfileModel();
            string email = form["User.EmailAddress"];
            model.User.EmailAddress = email;
            bool updates = bool.Parse(form["User.ReceiveUpdates"]);
            model.User.ReceiveUpdates = updates;

            model.User.Save();

            LogRepo.LogForUser(model.User.UserID, "Updated", "Account.UpdateProfile");

            return Profile();
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded = false;
                try
                {
                    User user = Settings.Current.User;

                    if (user == null)
                        throw new Exception("Invalid User!");

                    LogRepo.LogForUser(user.UserID, "Updated", "Account.ChangePassword");

                    changePasswordSucceeded = UserRepo.ChangePassword(user.UserID, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
