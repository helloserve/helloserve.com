using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Schedule
{
    public static class DateHelper
    {
        private static Func<DateTime> _currentDateFunc = () => { return DateTime.Now; };
        public static void SetCurrentDateFunc(Func<DateTime> func)
        {
            _currentDateFunc = func;
        }

        public static DateTime Now
        {
            get { return _currentDateFunc(); }
        }

        public static DateTime Today
        {
            get { return Now.Date; }
        }

        private static Func<DateTime> _currentUtcDateFunc = () => { return DateTime.UtcNow; };
        public static void SetCurrentUtcDateFunc(Func<DateTime> func)
        {
            _currentUtcDateFunc = func;
        }

        public static DateTime UtcNow
        {
            get { return _currentUtcDateFunc(); }
        }
    }
}
