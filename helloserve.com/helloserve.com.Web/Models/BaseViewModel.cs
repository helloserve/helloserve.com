using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace helloserve.com.Web.Models
{
    public class BaseViewModel
    {
        public List<FeatureViewModel> Features { get; set; }

        public BaseViewModel()
        {
            Features = FeatureViewModel.MockList;
        }
    }
}