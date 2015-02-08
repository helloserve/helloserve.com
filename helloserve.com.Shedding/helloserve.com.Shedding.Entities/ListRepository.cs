using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Shedding.Entities
{
    public class ListRepository : Base.BaseRepository
    {
        public IQueryable<SheddingStage> GetStages()
        {
            return Db.SheddingStages;
        }

        public SheddingStage GetStage(int stageId)
        {
            return Db.SheddingStages.SingleOrDefault(s => s.Id == stageId);
        }

        public IQueryable<Authority> GetAuthorities()
        {
            return Db.Authorities;
        }

        public Authority GetAuthority(int authorityId)
        {
            return Db.Authorities.Single(a => a.Id == authorityId);
        }

        public IQueryable<Area> GetAreas(int authorityId)
        {
            return Db.Areas.Where(a => a.AuthorityId == authorityId);
        }

        public Area GetArea(int areaId)
        {
            return Db.Areas.SingleOrDefault(a => a.Id == areaId);
        }
    }
}
