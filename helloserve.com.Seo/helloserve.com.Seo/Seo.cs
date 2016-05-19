using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml;

namespace helloserve.com.Seo
{
    public static class Seo
    {
        public static MvcHtmlString LoadSeo(this HtmlHelper html)
        {
            RazorView view = html.ViewContext.View as RazorView;
            
            if (view != null)
            {
                string path = view.ViewPath.Replace("~/", html.ViewContext.RequestContext.HttpContext.ApplicationInstance.Request.PhysicalApplicationPath).Replace("/", "\\");
                foreach (string ext in view.ViewStartFileExtensions)
                {
                    path = path.Replace(ext, "seo");
                }

                if (System.IO.File.Exists(path))
                {
                    XmlDocument seoXml = new XmlDocument();
                    seoXml.Load(path);

                    return new MvcHtmlString(seoXml["seo"].InnerXml);
                }
            }

            return null;
        }
    }
}
