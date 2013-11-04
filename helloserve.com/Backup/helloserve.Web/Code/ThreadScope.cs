using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace helloserve.Web
{
    public class ThreadScope : IDisposable
    {
        private static Dictionary<int, ThreadScope> _cache = new Dictionary<int, ThreadScope>();
        private static Dictionary<int, int> _refCount = new Dictionary<int, int>();

        private Dictionary<string, object> _items = new Dictionary<string, object>();

        private ThreadScope() {}

        public static ThreadScope Create()
        {
            int key = Thread.CurrentThread.ManagedThreadId;
            ThreadScope ts;
            if (_cache.ContainsKey(key))
            {
                _refCount[key]++;
                ts = _cache[key];
            }
            else
            {
                ts = new ThreadScope();
                _refCount.Add(key, 1);
                _cache.Add(key, ts);
            }
            return ts;
        }

        public static ThreadScope Current
        {
            get
            {
                int key = Thread.CurrentThread.ManagedThreadId;
                if (_cache.ContainsKey(key))
                    return _cache[key];
                return null;
            }
        }

        public object this[string key]
        {
            get
            {
                if (_items.ContainsKey(key))
                    return _items[key];
                return null;
            }
            set
            {
                if (_items.ContainsKey(key))
                    _items[key] = value;
                else
                    _items.Add(key, value);

            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            int key = Thread.CurrentThread.ManagedThreadId;
            if (_cache.ContainsKey(key))
            {
                if (_refCount[key] > 1)
                    return;

                IDisposable disp;
                foreach (var item in _items.Keys)
                {
                    disp = _items[item] as IDisposable;
                    if (disp != null) disp.Dispose();
                }
                _cache.Remove(key);
                _refCount.Remove(key);
            }
        }

        #endregion
    }
}
