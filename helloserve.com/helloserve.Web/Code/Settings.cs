using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.UI;
using System.IO;
using System.Configuration;
using helloserve.Common;
using System.Net;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace helloserve.Web
{
    public class Settings
    {
        private const string _screenCacheName = "ScreenItemsCache";
        private const int _numberOfPagerRows = 30;
        private string _sessionKey = string.Empty;

        public Settings(string sessionID)
        {
            _sessionKey = _screenCacheName + sessionID;
        }

        ///// <summary>
        ///// Log the user in and keep the user object for reference
        ///// </summary>
        ///// <param name="userName"></param>
        //public static bool Login(AuthenticationData auth)
        //{
        //    if (Current == null) Init();

        //    if (auth == null)
        //        return false;

        //    Current.AuthenticationData = auth;

        //    FormsAuthentication.SetAuthCookie(auth.UserName, false);
        //    return true;
        //}

        public static void Init()
        {
            string sessionID = HttpContext.Current.Session.SessionID;
            HttpContext.Current.Session["Settings"] = new Settings(sessionID);
        }

        public static void Logout()
        {
            // clear the menu item cache 
            //Current.MainTreeMenuScreens = null;

            FormsAuthentication.SignOut();
            HttpContext.Current.Session.Clear();
        }

        public static helloserveContext DB
        {
            get
            {
                if (HttpContext.Current.Items["__DB__"] == null)
                {
                    Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", ConfigurationManager.AppSettings["DataPath"], string.Empty);
                    Database.SetInitializer(new DropCreateDatabaseIfModelChanges<helloserveContext>());

                    helloserveContext db = new helloserveContext();

                    HttpContext.Current.Items["__DB__"] = db;
                }
                return HttpContext.Current.Items["__DB__"] as helloserveContext;
            }
        }

        public static List<Forum> Forums
        {
            get
            {
                return ForumRepo.GetAll().ToList();
            }
        }

        public int ForumPages
        {
            get
            {
                if (helloserve.Web.ThreadScope.Current != null)
                {
                    return int.Parse(helloserve.Web.ThreadScope.Current["ForumPages"] as string);
                }
                else
                    return int.Parse((string)G("ForumPages"));
            }
            set
            {
                if (helloserve.Web.ThreadScope.Current != null)
                    helloserve.Web.ThreadScope.Current["ForumPages"] = value.ToString();
                else
                    S("ForumPages", value.ToString());
            }
        }

        public static Settings Current
        {
            get
            {
                if (helloserve.Web.ThreadScope.Current != null)
                {
                    Settings set = helloserve.Web.ThreadScope.Current["Settings"] as Settings;

                    return set;
                }
                else
                {
                    Settings.Init();
                    
                    Settings current = HttpContext.Current.Session["Settings"] as Settings;
                    
                    current.ForumPages = int.Parse(ConfigurationManager.AppSettings["ForumPages"]);
                    
                    return current;
                }
            }
        }

        //public AuthenticationData AuthenticationData
        //{
        //    get
        //    {
        //        if (STP.Web.ThreadScope.Current != null)
        //        {
        //            return STP.Web.ThreadScope.Current["AuthenticationData"] as AuthenticationData;
        //        }
        //        else
        //            return G("AuthenticationData") as AuthenticationData;
        //    }
        //    set
        //    {
        //        if (STP.Web.ThreadScope.Current != null)
        //            STP.Web.ThreadScope.Current["AuthenticationData"] = value;
        //        else
        //            S("AuthenticationData", value);
        //    }
        //}

        //public string RedirectURL
        //{
        //    get
        //    {
        //        if (STP.Web.ThreadScope.Current != null)
        //        {
        //            return STP.Web.ThreadScope.Current["RedirectURL"].ToString();
        //        }
        //        else
        //            return G("RedirectURL").ToString();
        //    }
        //    set
        //    {
        //        if (STP.Web.ThreadScope.Current != null)
        //            STP.Web.ThreadScope.Current["RedirectURL"] = value;
        //        else
        //            S("RedirectURL", value);
        //    }
        //}

        public int? GetUserID()
        {
            if (Current.User != null)
                return Current.User.UserID;
            else
                return null;
        }

        public User User
        {
            get
            {
                if (helloserve.Web.ThreadScope.Current != null)
                {
                    return helloserve.Web.ThreadScope.Current["User"] as User;
                }
                else
                    return G("User") as User;
            }
            set
            {
                if (helloserve.Web.ThreadScope.Current != null)
                    helloserve.Web.ThreadScope.Current["User"] = value;
                else
                    S("User", value);
            }
        }

        public bool IsAdminUser
        {
            get
            {
                if (Current.User == null)
                    return false;

                return Current.User.Administrator;
            }
        }

        //public static bool HasUserRole(RoleTypeEnum role)
        //{
        //    return ((int)role & Current.User.RoleBW) > 0;
        //}

        private object G(string key) { return HttpContext.Current.Session[key]; }
        private void S(string key, object value) { HttpContext.Current.Session[key] = value; }

        public const string DateDisplayFormat = "yyyy-MM-dd";

        public static int NumberOfPagerRows
        {
            get { return _numberOfPagerRows; }
        }
    }
}