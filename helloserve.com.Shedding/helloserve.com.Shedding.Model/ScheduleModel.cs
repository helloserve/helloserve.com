using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Shedding.Model
{
    public class ScheduleModel
    {
        public int AreaId { get; set; }
        public int StageId { get; set; }
        public bool IsShedding { get; set; }

        public List<ScheduleCalendarModel> Calendar { get; set; }

        public ScheduleModel()
        {
            Calendar = new List<ScheduleCalendarModel>();
        }

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
            List<Entities.ScheduleCalendar> calendarItems = authority.GetSchedule(areaId, stageId.Value);

            ScheduleModel model = new ScheduleModel();

            model.AreaId = areaId;
            model.StageId = stageId.Value;
            foreach (Entities.ScheduleCalendar calendarItem in calendarItems)
            {
                model.Calendar.Add(new ScheduleCalendarModel()
                {
                    StartTime = calendarItem.StartTime,
                    EndTime = calendarItem.EndTime
                });

                model.IsShedding |= (calendarItem.StartTime <= DateTime.UtcNow && calendarItem.EndTime >= DateTime.UtcNow);
            }

            return model;
        }
    }

    public class ScheduleCalendarModel
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
