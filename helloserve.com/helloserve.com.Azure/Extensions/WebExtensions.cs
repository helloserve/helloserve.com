using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Schema;

namespace helloserve.com.Azure
{
    public static class WebExtensions
    {
        public static string StaticlinkContent(this HtmlHelper html, string content)
        {
            string mediaUrl = ConfigurationManager.AppSettings["mediaLocationUrl"];
            return content.Replace(string.Format("/{0}", mediaUrl), string.Format("{0}{1}", html.ViewBag.BaseUrl, mediaUrl));
        }

        public static string ToRgba(this string color, double a)
        {
            if (string.IsNullOrEmpty(color))
                return string.Empty;

            if (color.StartsWith("#"))
                color = color.Remove(0, 1);

            string sr;
            string sg;
            string sb;
            int r;
            int g;
            int b;
            if (color.Length == 3)
            {
                sr = color.Substring(0, 1);
                sg = color.Substring(1, 1);
                sb = color.Substring(2, 1);
            }
            else
            {
                sr = color.Substring(0, 2);
                sg = color.Substring(2, 2);
                sb = color.Substring(4, 2);
            }

            r = Convert.ToInt32(sr, 16);
            g = Convert.ToInt32(sg, 16);
            b = Convert.ToInt32(sb, 16);

            return string.Format("rgba({0}, {1}, {2}, {3})", r, g, b, a);
        }

        public static MvcHtmlString SVG(this HtmlHelper htmlHelper, string path, object htmlAttributes)
        {
            Dictionary<String, Object> attributes = new Dictionary<String, Object>();
            var fullPath = HttpContext.Current.Server.MapPath(path);
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(fullPath);
            PropertyInfo[] properties = htmlAttributes.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in properties)
            {
                if (xmlDoc.DocumentElement.Attributes[propertyInfo.Name] != null)
                {
                    xmlDoc.DocumentElement.Attributes[propertyInfo.Name].Value =
                        (string)propertyInfo.GetValue(htmlAttributes, null);
                }
                else
                {
                    XmlAttribute xsiNil = xmlDoc.CreateAttribute(propertyInfo.Name);
                    xsiNil.Value = (string)propertyInfo.GetValue(htmlAttributes, null);
                    xmlDoc.DocumentElement.Attributes.Append(xsiNil);
                }
            }
            return new MvcHtmlString(xmlDoc.OuterXml);
        }
    }
}