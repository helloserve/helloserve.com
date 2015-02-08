using helloserve.com.Shedding.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;

namespace helloserve.com.Shedding.Api.Models
{
    public class ShedSession
    {
        #region STATIC

        private static ConcurrentDictionary<string, ShedSession> _sessions;

        static ShedSession()
        {
            _sessions = new ConcurrentDictionary<string, ShedSession>();
        }

        public static ShedSession GetSession(HttpRequestHeaders headers)
        {
            var header = headers.Where(h => h.Key.ToUpper() == "LOADSHEDDEE").SingleOrDefault();
            string number = header.Value.FirstOrDefault();

            ShedSession session;
            if (!string.IsNullOrEmpty(number))
            {
                session = new ShedSession(number);
                _sessions.AddOrUpdate(number, session, (k, v) => { return session; });
            }

            if (_sessions.TryGetValue(number, out session))
                return session;

            throw new UnauthorizedAccessException();
        }

        private static void SetSession(ShedSession session)
        {
            _sessions.AddOrUpdate(session._phoneNumber, session, (k, v) => { return session; });
        }

        #endregion

        ShedSession(string phoneNumber)
        {
            _phoneNumber = phoneNumber;
            User = UserModel.Get(phoneNumber);
        }

        private string _phoneNumber;

        private UserModel _user;
        public UserModel User
        {
            get { return _user; }
            set
            {
                _user = value;
                Set();
            }
        }

        public void Set()
        {
            SetSession(this);
        }
    }
}