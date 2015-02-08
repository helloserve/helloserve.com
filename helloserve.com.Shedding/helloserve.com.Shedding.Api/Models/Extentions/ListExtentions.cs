using helloserve.com.Shedding.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace helloserve.com.Shedding.Api.Models
{
    public static class ListExtentions
    {
        public static StageDetail AsDetail(this StageModel model)
        {
            return new StageDetail()
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        public static List<StageDetail> ToDetailList(this List<StageModel> list)
        {
            List<StageDetail> detailList = new List<StageDetail>();
            list.ForEach(l => { detailList.Add(l.AsDetail()); });
            return detailList;
        }

        public static AuthorityDetail AsDetail(this AuthorityModel model)
        {
            return new AuthorityDetail()
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        public static List<AuthorityDetail> ToDetailList(this List<AuthorityModel> list)
        {
            List<AuthorityDetail> detailList = new List<AuthorityDetail>();
            list.ForEach(l => { detailList.Add(l.AsDetail()); });
            return detailList;
        }

        public static AreaDetail AsDetail(this AreaModel model)
        {
            return new AreaDetail()
            {
                Id = model.Id,
                Name = model.Name,
                Code = model.Code
            };
        }

        public static List<AreaDetail> ToDetailList(this List<AreaModel> list)
        {
            List<AreaDetail> detailList = new List<AreaDetail>();
            list.ForEach(l => { detailList.Add(l.AsDetail()); });
            return detailList;
        }
    }
}