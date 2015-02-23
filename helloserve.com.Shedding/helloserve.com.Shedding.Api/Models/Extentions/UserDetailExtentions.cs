using helloserve.com.Shedding.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace helloserve.com.Shedding.Api.Models
{
    public static class UserDetailExtentions
    {
        public static UserDetail AsDetail(this UserModel model)
        {
            return new UserDetail()
            {
                NotificationPeriod = model.NotificationPeriod                
            };
        }
    }
}