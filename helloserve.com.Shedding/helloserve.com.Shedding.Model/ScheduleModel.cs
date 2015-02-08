using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Shedding.Model
{
    public class ScheduleModel
    {
        public int AreaId;
        public int StageId;
        public bool IsShedding;

        public static ScheduleModel GetSchedule(int userId, int areaId, int? stageId = null)
        {
            UserAreaModel area = UserAreaModel.Get(userId).SingleOrDefault(a => a.AreaId == areaId);

            if (area == null || !area.AuthorityId.HasValue)
                throw new InvalidOperationException();

            AuthorityModel authority = AuthorityModel.Get(area.AuthorityId.Value);
            if (!stageId.HasValue)
            {
                stageId = (int?)Cache.ModelCache.Get("EskomStage");

                if (!stageId.HasValue)
                {
                    SheddingStage? currentStage = authority.GetCurrentStage();
                    if (currentStage == null)
                        throw new InvalidOperationException("Could not determine stage");
                    
                    stageId = (int)currentStage;
                    Cache.ModelCache.Add("EskomStage", stageId.Value);
                }
            }
            
            authority.CalculateSchedule(areaId, stageId.Value);
            
            //load schedule
            return new ScheduleModel();
        }
    }
}
