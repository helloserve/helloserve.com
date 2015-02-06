using helloserve.com.Shedding.Api.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace helloserve.com.Shedding.Api.Controllers
{
    [RoutePrefix("user")]
    public class UserController : ApiController
    {
        public void Put(string number)
        {
            //TODO: register the user
        }

        public void Post([FromBody]UserDetail data)
        {
            //TODO: update the user
        }

        public UserDetail Get()
        {
            //TODO: return the user
            return new UserDetail();
        }
    }
}
