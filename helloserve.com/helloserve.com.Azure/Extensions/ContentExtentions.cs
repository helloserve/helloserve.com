using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace helloserve.com.Azure.Extensions
{
    public static class ContentExtentions
    {
        public static string ToUrlString(this string value)
        {
            return value.Replace(" ", "-").Replace("?", "").Replace("!", "").Replace(",", "").Replace(".", "").Replace("'", "").Replace("’", "");
        }
    }
}