using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace helloserve.com.Web.Models.Data
{
    public class UserDataModel : BaseViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public bool Authenticate()
        {
            return Model.User.Authenticate(this.AsModel());
        }
    }
}