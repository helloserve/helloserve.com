using helloserve.com.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Repository
{
    public class UserRepository : Base.BaseRepository
    {
        public IQueryable<User> GetUsersByUsername(string username)
        {
            return Db.Users.Where(u => u.Username.ToLower() == username.ToLower());
        }
    }
}
