using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace helloserve.com.Web
{
    public class AdminSession
    {
        private static AdminSession _session;

        public static AdminSession Instance
        {
            get
            {
                _session = HttpContext.Current.Session["adminSession"] as AdminSession;
                if (_session == null)
                {
                    _session = new AdminSession();
                    _session.Save();
                }

                return _session;
            }
        }

        public void Save()
        {
            HttpContext.Current.Session["adminSession"] = this;
        }

        public bool Authenticated { get; set; }
        public string RedirectPath { get; set; }
    }
}