using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Shedding.Entities.Base
{
    public class BaseRepository
    {
        private LoadShedEntities _db;
        public LoadShedEntities Db
        {
            get
            {
                if (_db == null)
                    _db = new LoadShedEntities();
                return _db;
            }
        }
    }
}
