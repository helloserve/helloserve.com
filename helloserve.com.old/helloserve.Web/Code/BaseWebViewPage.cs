using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using System.Text;

namespace helloserve.Web
{
    public abstract class BaseWebViewPage<TModel> : WebViewPage<TModel>
    {
        public override bool IsAjax { get { return Request.IsAjaxRequest(); } }
        public int CurrentPageID { get { return (int)ViewContext.RouteData.Values["id"]; } }
        /*
        public Version AppVersion
        {
            get
            {
                Assembly assm = Assembly.GetExecutingAssembly();
                return assm.GetName().Version;
            }
        }
        */
                
        public MvcHtmlString IncludeControllerScript()
        {
            return MvcHtmlString.Create("<script type=\"text/javascript\" src=\"" + Url.Content("~/Scripts/Views/" + ViewContext.RouteData.Values["controller"] + ".js") + "\"></script>");
        }

        public MvcHtmlString IncludeJS(params string[] jsfiles)
        {
            StringBuilder sb = new StringBuilder(512);
            foreach (var file in jsfiles)
                sb.Append("<script type=\"text/javascript\" src=\"" + Url.Content("~/Scripts/" + file) + "\"></script>");
            return MvcHtmlString.Create(sb.ToString());
        }

        public MvcHtmlString IncludeCSS(params string[] cssFiles)
        {
            StringBuilder sb = new StringBuilder(512);
            foreach (var file in cssFiles)
                sb.Append("<link href=\"" + Url.Content("~/Content/" + file) + "\" rel=\"stylesheet\" type=\"text/css\" />");
            return MvcHtmlString.Create(sb.ToString());
        }

        public MvcHtmlString IncludeRichEditor()
        {
            return MvcHtmlString.Create("<script type=\"text/javascript\" src=\"" + Url.Content("~/Content/ckeditor/ckeditor.js") + "\"></script>");
        }
    }

    public abstract class BaseWebViewPage : BaseWebViewPage<dynamic>
    {
    }
}