using helloserve.com.Shedding.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace helloserve.com.Shedding.Api.Controllers.Base
{
    public class BaseApiController : ApiController
    {
        private ShedSession _session;
        public ShedSession Session
        {
            get { return _session; }
            set { _session = value; }
        }
    }
}
