using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace helloserve.com.Web
{
    public static class WebExtensions
    {
        public static string StaticlinkContent(this HtmlHelper html, string content)
        {
            string mediaUrl = ConfigurationManager.AppSettings["mediaLocationUrl"];
            return content.Replace(string.Format("/{0}", mediaUrl), string.Format("{0}{1}", html.ViewBag.BaseUrl, mediaUrl));
        }
    }
}