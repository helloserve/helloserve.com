using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(helloserve.com.Azure.Startup))]
namespace helloserve.com.Azure
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
