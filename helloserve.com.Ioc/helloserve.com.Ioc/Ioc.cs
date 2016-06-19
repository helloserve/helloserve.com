using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Ioc
{
    public class Ioc : IocManager
    {
        private static Ioc _instance;

        public static Ioc Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Ioc();

                return _instance;
            }
        }        
    }
}
