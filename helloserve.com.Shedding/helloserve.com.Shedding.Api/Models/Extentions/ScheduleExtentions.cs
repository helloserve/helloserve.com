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
                IsCurrentlyShedding = model.IsShedding
            };
        }
    }
}