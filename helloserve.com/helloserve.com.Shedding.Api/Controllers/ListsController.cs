using helloserve.com.Shedding.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace helloserve.com.Shedding.Api.Controllers
{
    [RoutePrefix("lists")]
    public class ListsController : ApiController
    {
        public ListDetail Get()
        {
            return new ListDetail();
        }

        public List<AreaDetail> Get(string authority)
        {
            return new List<AreaDetail>();
        }
    }
}
