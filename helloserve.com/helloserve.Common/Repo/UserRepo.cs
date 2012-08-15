using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace helloserve.Common
{
    public class UserRepo : BaseRepo<User>
    {
        public static void CheckDefaultUser()
        {
            var defaultUser = (from u in DB.Users
                               where u.Username == "helloserve"
                               select u).SingleOrDefault();

            if (defaultUser == null)
            {
                defaultUser = new User()
                {
                    Username = "helloserve",
                    Password = "win32api",
                    EmailAddress = "helloserve@gmail.com",
                    ReceiveUpdates = false,
                    Administrator = true
                };

                defaultUser.Save();
            };

        }

        public static User ValidateUser(string username, string password)
        {
            var user = (from u in DB.Users
                        where u.Username == username && u.Password == password
                        select u).SingleOrDefault();

            return user;
        }

        public static User ValidateUser(string username)
        {
            var user = (from u in DB.Users
                        where u.Username == username
                        select u).SingleOrDefault();

            return user;
        }

        public static User RegisterUser(string username, string password, string email)
        {
            User user = new User()
            {
                Username = username,
                Password = password,
                EmailAddress = email,
                ReceiveUpdates = false,
                Administrator = false
            };

            user.Save();

            return user;
        }

        public static bool ChangePassword(int userID, string newPassword)
        {
            User user = (from u in DB.Users
                         where u.UserID == userID
                         select u).SingleOrDefault();

            if (user == null)
                return false;

            user.Password = newPassword;
            user.Save();

            return true;
        }
    }
}
