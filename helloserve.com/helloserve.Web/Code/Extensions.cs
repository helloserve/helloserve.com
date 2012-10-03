using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.IO;
using System.Web.Routing;
using System.Text.RegularExpressions;

namespace helloserve.Web
{
    public static class Extensions
    {
        #region DATE TIME FUNCTIONS

        public static string ToProcessMonth(this DateTime date)
        {
            return date.ToString("yyyyMM");
        }

        public static string ToDisplayString(this DateTime? date)
        {
            if (!date.HasValue) return string.Empty;
            return date.Value.ToDisplayString();
        }

        public static string ToDisplayString(this DateTime date)
        {
            return date.ToString(Settings.DateDisplayFormat);
        }

        #endregion

        #region DECIMAL FUNCTIONS

        public static string ToDecimalString(this decimal value)
        {
            return value.ToString("#,###,##0.00");
        }

        #endregion

        #region HTML HELPER FUNCTIONS

        public static MvcHtmlString DatePicker(this HtmlHelper helper, string name, object value, object htmlAttributes = null)
        {
            var attributeList = (htmlAttributes == null) ? new RouteValueDictionary() : new RouteValueDictionary(htmlAttributes);
            updateHtmlAttr(attributeList, "datePicker");
            return InputExtensions.TextBox(helper, name, value, attributeList);
        }

        #endregion

        #region CONTROLLER HELPER FUNCTIONS

        public static string RenderPartialView(this Controller controller, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = controller.ControllerContext.RouteData.GetRequiredString("action");

            controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.ToString();
            }
        }

        #endregion

        private static void updateHtmlAttr(IDictionary<string, object> htmlAttr, string extraClass = "", string extraStyle = "")
        {
            if (htmlAttr == null) return;

            if (extraClass.Length > 0)
            {
                if (!htmlAttr.ContainsKey("class"))
                {
                    htmlAttr.Add("class", "");
                }

                htmlAttr["class"] += " " + extraClass;
            }

            if (extraStyle.Length > 0)
            {
                if (!htmlAttr.ContainsKey("style"))
                {
                    htmlAttr.Add("style", "");
                }

                htmlAttr["style"] += extraStyle;
            }
        }

        #region FORUM POST
        public static string ForumPost(this HtmlHelper helper, string post)
        {
            string markupPost = post;
            //try for bold
            Regex regex = new Regex(@"\s?(_(.*)_)\s?");
            Match match = regex.Match(markupPost);
            while (match.Success)
            {
                markupPost = markupPost.Replace(match.Groups[0].Value, " <i>" + match.Groups[2].Value + "</i> ");
                //markupPost.Remove(match.Index, match.Length);
                //markupPost = markupPost.Insert(match.Index, " <i>" + match.Groups[2].Value + "</i> ");

                match = match.NextMatch();
            }

            //try for italic
            regex = new Regex(@"\s?(\*(.*)\*)\s?");
            match = regex.Match(markupPost);
            while (match.Success)
            {
                markupPost = markupPost.Replace(match.Groups[0].Value, " <strong>" + match.Groups[2].Value + "</strong> ");
                //markupPost = markupPost.Remove(match.Index, match.Length);
                //markupPost = markupPost.Insert(match.Index, " <strong>" + match.Groups[2].Value + "</strong> ");

                match = match.NextMatch();
            }

            //try for link 
            string postPart = markupPost;
            regex = new Regex(@"http://\S+\s?");
            match = regex.Match(markupPost);
            while (match.Success)
            {
                markupPost = markupPost.Replace(match.Groups[0].Value, string.Format(" <a href=\"{0}\">{0}</a> ", match.Groups[0].Value.Replace("\r\n", "").Replace("\r", "").Replace("\r\n", "")));
                //markupPost = markupPost.Remove(match.Index, match.Length);
                //markupPost = markupPost.Insert(match.Index, string.Format(" <a href=\"{0}\">{0}</a> ", match.Groups[0].Value.Replace("\r\n", "").Replace("\r", "").Replace("\r\n", "")));

                match = match.NextMatch();
            }

            markupPost = "<p>" + markupPost;
            markupPost = markupPost.Replace("\r\n\r\n", "</p><p>").Replace("\r\n", "<br />").Replace("\r", "<br />").Replace("\n", "<br />");
            markupPost += "</p>";

            return markupPost;
        }
        #endregion
    }
}