using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using helloserve.Common;
using System.IO;
using System.Configuration;

namespace helloserve.Web
{
    public static class Email
    {
        public static void SendActivation(User user)
        {
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage("accounts@helloserve.com", user.EmailAddress);

            string path = Path.Combine(ConfigurationManager.AppSettings["TemplatePath"], "activation.htm");
            string body = File.ReadAllText(path);
            body = body.Replace("@[username]", user.Username);
            body = body.Replace("@[link]", string.Format("http://{0}/Account/Activate/{1}", HttpContext.Current.Request.Url.Host, user.ActivationToken));

            message.Subject = "helloserve.com Account Activation";
            message.Body = body;
            message.IsBodyHtml = true;
            message.BodyEncoding = System.Text.Encoding.UTF8;

            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(ConfigurationManager.AppSettings["SMTPServer"]);
            int port = 0;
            if (int.TryParse(ConfigurationManager.AppSettings["SMPTPort"], out port))
            {
                client.Port = int.Parse(ConfigurationManager.AppSettings["SMTPPort"]);
            }
            client.EnableSsl = bool.Parse(ConfigurationManager.AppSettings["SMTPSSL"]);
            
            string username = ConfigurationManager.AppSettings["SMTPUsername"];
            if (!string.IsNullOrEmpty(username))
            {
                client.Credentials = new System.Net.NetworkCredential(username, ConfigurationManager.AppSettings["SMTPPassword"]);
            }
            client.Send(message);

            //LogRepo.LogForUser(user.UserID, "Activation to " + user.EmailAddress, "Email.SendActivation");
            Settings.EventLogger.Log(EventLogEntry.LogForUser(user.UserID, string.Format("Activation to {0}", user.EmailAddress), "Email.SendActivation"));
        }

        public static void SendResetConfirmation(User user, string newPassword)
        {
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage("accounts@helloserve.com", user.EmailAddress);

            string path = Path.Combine(ConfigurationManager.AppSettings["TemplatePath"], "reset.htm");
            string body = File.ReadAllText(path);
            body = body.Replace("@[username]", user.Username);
            body = body.Replace("@[password]", newPassword);

            message.Subject = "helloserve.com Account Reset";
            message.Body = body;
            message.IsBodyHtml = true;
            message.BodyEncoding = System.Text.Encoding.UTF8;

            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(ConfigurationManager.AppSettings["SMTPServer"]);
            int port = 0;
            if (int.TryParse(ConfigurationManager.AppSettings["SMTPPort"], out port))
            {
                client.Port = int.Parse(ConfigurationManager.AppSettings["SMTPPort"]);
            }
            client.EnableSsl = bool.Parse(ConfigurationManager.AppSettings["SMTPSSL"]);

            string username = ConfigurationManager.AppSettings["SMTPUsername"];
            if (!string.IsNullOrEmpty(username))
            {
                client.Credentials = new System.Net.NetworkCredential(username, ConfigurationManager.AppSettings["SMTPPassword"]);
            }
            client.Send(message);

            //LogRepo.LogForUser(user.UserID, "Password Reset to " + user.EmailAddress, "Email.SendResetConfirmation");
            Settings.EventLogger.Log(EventLogEntry.LogForUser(user.UserID, string.Format("Password Reset to {0}", user.EmailAddress), "Email.SendResetConfirmation"));
        }
    }
}