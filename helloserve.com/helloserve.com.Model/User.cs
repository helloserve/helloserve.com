using helloserve.com.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Model
{
    public class User : Entities.User
    {
        public string UserPassword { get; set; }

        public static bool Authenticate(User model)
        {
            string userPassword = UTF8Encoding.UTF8.GetString(GetMD5Hash(model.UserPassword));

            UserRepository repo = new UserRepository();
            List<User> users = repo.GetUsersByUsername(model.Username).ToModelList();
            string savedPassword;
            foreach (Entities.User user in users)
            {
                savedPassword = UTF8Encoding.UTF8.GetString(user.Password);

                if (userPassword == savedPassword)
                    return true;
            }

            return false;
        }

        private static byte[] GetMD5Hash(string str)
        {
            MD5 md5 = MD5.Create();
            byte[] bytes = Encoding.Default.GetBytes(str);
            bytes = md5.ComputeHash(bytes);
            return bytes;
        }

    }
}
