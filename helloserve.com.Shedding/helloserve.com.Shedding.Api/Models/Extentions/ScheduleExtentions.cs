using helloserve.com.Shedding.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace helloserve.com.Shedding.Api.Models
{
    public static class ScheduleExtentions
    {
        public static UserAreaDetail AsDetail(this UserAreaModel model)
        {
            return new UserAreaDetail()
            {
                Id = model.Id,
                UserId = model.UserId,
                AreaId = model.AreaId,
                Area = AreaModel.Get(model.AreaId).AsDetail(),
                AuthorityId = model.AuthorityId
            };
        }

        public static List<UserAreaDetail> ToDetailList(this List<UserAreaModel> list)
        {
            List<UserAreaDetail> detailList = new List<UserAreaDetail>();
            list.ForEach(l => { detailList.Add(l.AsDetail()); });
            return detailList;
        }

        public static ScheduleDetail AsDetail(this ScheduleModel model)
        {
            return new ScheduleDetail()
            {
                AreaId = model.AreaId,
                StageId = model.StageId,
                IsCurrentlyShedding = model.IsShedding,
                CurrentShedding = model.IsShedding ? model.Calendar.SingleOrDefault(c=>c.StartTime <= DateTime.UtcNow && c.EndTime >= DateTime.UtcNow).AsDetail(model.AreaId, model.StageId) : null,
                FutureShedding = model.Calendar.Where(c=>c.StartTime > DateTime.UtcNow).ToList().ToDetailList(model.AreaId, model.StageId)
            };
        }

        public static SheddingDetail AsDetail(this ScheduleCalendarModel model, int areaId, int stageId)
        {
            return new SheddingDetail()
            {
                AreaId = areaId,
                StageId = stageId,
                StartTime = model.StartTime,
                StopTime = model.EndTime
            };
        }

        public static List<SheddingDetail> ToDetailList(this List<ScheduleCalendarModel> list, int areaId, int stageId)
        {
            List<SheddingDetail> detailList = new List<SheddingDetail>();
            list.ForEach(l => { detailList.Add(l.AsDetail(areaId, stageId)); });
            return detailList;
        }
    }
}