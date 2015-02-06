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
        private static ConcurrentDictionary<string, ShedSession> _sessions;

        static ShedSession()
        {
            _sessions = new ConcurrentDictionary<string, ShedSession>();
        }

        public static ShedSession GetSession(HttpContentHeaders headers)
        {
            
            var header = headers.Where(h => h.Key.ToUpper() == "LOADSHEDDEE").SingleOrDefault();
            string number = header.Value.FirstOrDefault();

            ShedSession session;
            if (string.IsNullOrEmpty(number))
            {
                //TODO: create user


                session = new ShedSession();
                _sessions.AddOrUpdate(number, session, (k, v) => { return session; });
            }

            if (_sessions.TryGetValue(number, out session))
                return session;

            throw new UnauthorizedAccessException();
        }
    }
}