using helloserve.com.Web.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace helloserve.com.Web.Models
{
    public class BaseViewModel
    {
        public CollectionViewModel Features { get; set; }

        public BaseViewModel()
        {
            Features = FeatureDataModel.MockList;
        }
    }
}