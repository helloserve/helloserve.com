using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace helloserve.com.Shedding.Api.Models
{
    public class ListDetail
    {
        public List<StageDetail> Stages { get; set; }
        public List<AuthorityDetail> Authorities { get; set; }
    }
}