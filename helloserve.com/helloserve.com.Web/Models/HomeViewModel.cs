using helloserve.com.Web.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace helloserve.com.Web.Models
{
    public class HomeViewModel : BaseViewModel
    {
        public ContentDataModel NewsItem { get; set; }

        public override void Load()
        {
            base.Load();
            NewsItem = NewsDataModel.MockList.ListItems.FirstOrDefault();
        }
    }
}