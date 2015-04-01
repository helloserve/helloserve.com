using helloserve.com.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Repository
{
    public class FeatureRepository : Base.BaseRepository
    {
        public IQueryable<Feature> GetAll()
        {
            return Db.Features;
        }

        public Feature Get(int featureId)
        {
            return GetAll().Single(f => f.FeatureID == featureId);
        }
    }
}
