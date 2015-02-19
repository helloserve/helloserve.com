using helloserve.com.Web.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace helloserve.com.Web.Models
{
    public class HomeViewModel : BaseViewModel
    {
        public CollectionViewModel NewsItems { get; set; }        

        public HomeViewModel()
        {
            NewsItems = NewsDataModel.MockList;
        }
    }
}