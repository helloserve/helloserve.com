using helloserve.com.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Repository.Base
{
    public class BaseRepository
    {
        private helloserveEntities _db;
        protected helloserveEntities Db
        {
            get
            {
                if (_db == null)
                    _db = new helloserveEntities();
                return _db;
            }
        }
    }
}
