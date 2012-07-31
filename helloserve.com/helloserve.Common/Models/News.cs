using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace helloserve.Common.Models
{
    public class News
    {
        public int NewsID { get; internal set; }
        public int? FeatureID { get; set; }
        public string Title { get; set; }
        public string Cut { get; set; }
        public string Post { get; set; }
    }
}
