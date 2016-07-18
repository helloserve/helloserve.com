using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Ioc
{
    public abstract class IocManager
    {
        private Dictionary<string, Func<object>> _funcs = new Dictionary<string, Func<object>>();

        public void RegisterTypeInstance<T>(Func<T> func)
        {
            Type type = typeof(T);
            string name = type.FullName;

            if (!_funcs.ContainsKey(name))
                _funcs.Add(name, func as Func<object>);
            else
                _funcs[name] = func as Func<object>;
        }

        public T GetTypeInstance<T>()
        {
            Type type = typeof(T);
            string name = type.FullName;

            if (_funcs.ContainsKey(name))
                return (_funcs[name] as Func<T>)();

            throw new KeyNotFoundException(string.Format("Type '{0}' not registered.", name));
        }
    }
}
