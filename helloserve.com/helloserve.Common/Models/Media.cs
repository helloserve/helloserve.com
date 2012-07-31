using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace helloserve.Common.Models
{
    public class Media
    {
        public int MediaID { get; internal set; }
        public int MediaType { get; set; }
        public string Location { get; set; }
    }
}
