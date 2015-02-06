using helloserve.com.Shedding.Api.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace helloserve.com.Shedding.Api.Controllers
{
    [RoutePrefix("schedule")]
    public class ScheduleController : ApiController
    {
        // POST schedule/234
        public void Post(int areaId)
        {
            //TODO: update UserArea
        }

        public ScheduleDetail Get(int areaId, int? stageId)
        {
            return new ScheduleDetail();
        }
    }
}