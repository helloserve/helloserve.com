using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace helloserve.Common.Models
{
    public class Feature
    {
        public int FeatureID { get; internal set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
