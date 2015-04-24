using helloserve.com.Web.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace helloserve.com.Web.Models
{
    public static class UserExtensions
    {
        public static Model.User AsModel(this UserDataModel dataModel)
        {
            return new Model.User()
            {
                Username = dataModel.Username,
                UserPassword = dataModel.Password
            };
        }

    }
}