using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Model
{
    public static class UserExtensions
    {
        public static User AsModel(this Entities.User entity)
        {
            return new User()
            {
                Username = entity.Username,
                Password = entity.Password
            };
        }

        public static List<User> ToModelList(this IEnumerable<Entities.User> collection)
        {
            List<User> list = new List<User>();
            foreach (Entities.User user in collection)
            {
                list.Add(user.AsModel());
            }
            return list;
        }
    }
}
