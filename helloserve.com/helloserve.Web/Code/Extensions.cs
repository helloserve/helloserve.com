using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.IO;
using System.Web.Routing;

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
    }
}