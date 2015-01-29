using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace helloserve.Common
{
    public class UserRepo : BaseRepo<User>
    {
        public static byte[] GetMD5Hash(string str)
        {
            MD5 md5 = MD5.Create();
            byte[] bytes = Encoding.Default.GetBytes(str);
            bytes = md5.ComputeHash(bytes);
            return bytes;
        }

        public static string ResetPassword(User user)
        {
            byte[] bytes = new byte[10];
            Random rnd = new Random();
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = (byte)((int)rnd.Next(93) + 33);
            }

            string s = ASCIIEncoding.ASCII.GetString(bytes);
            user.Password = UserRepo.GetMD5Hash(s);
            user.Save();

            return s;
        }

        public static User GetByID(int id)
        {
            User user = DB.Users.Where(u => u.UserID == id).SingleOrDefault();
            return user;
        }

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
                    Password = GetMD5Hash("win32api"),
                    EmailAddress = "helloserve@gmail.com",
                    ReceiveUpdates = false,
                    Administrator = true,
                    ActivationToken = Guid.NewGuid(),
                    Activated = true
                };

                defaultUser.Save();
            };

        }

        public static User ValidateUser(string username, string password)
        {
            byte[] pwd = GetMD5Hash(password);

            var user = (from u in DB.Users
                        where u.Username == username && u.Password == pwd && u.Activated == true
                        select u).SingleOrDefault();

            return user;
        }

        public static User ValidateUser(string username, bool activated)
        {
            var user = (from u in DB.Users
                        where u.Username == username && ((activated && u.Activated == true) || !activated)
                        select u).SingleOrDefault();

            return user;
        }

        public static User RegisterUser(string username, string password, string email, bool recieveUpdates)
        {
            User user = UserRepo.ValidateUser(username, false);
            if (user != null)
                return null;

            user = new User()
            {
                Username = username,
                Password = GetMD5Hash(password),
                EmailAddress = email,
                ReceiveUpdates = recieveUpdates,
                Administrator = false,
                ActivationToken = Guid.NewGuid(),
                Activated = false
            };

            user.Save();

            return user;
        }

        public static User ActivateUser(Guid guid)
        {
            var user = (from u in DB.Users
                        where u.Activated == false && u.ActivationToken == guid
                        select u).SingleOrDefault();

            return user;
        }

        public static bool ChangePassword(int userID, string newPassword)
        {
            User user = (from u in DB.Users
                         where u.UserID == userID
                         select u).SingleOrDefault();

            if (user == null)
                return false;

            user.Password = GetMD5Hash(newPassword);
            user.Save();

            return true;
        }
    }
}
