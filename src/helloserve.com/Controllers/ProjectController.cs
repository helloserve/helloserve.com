using helloserve.com.Adaptors;
using helloserve.com.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace helloserve.com.Controllers
{
    [Route("projects/")]
    public class ProjectController : Controller
    {
        readonly DomainOptions options;
        readonly IProjectServiceAdaptor adaptor;
        readonly IPageState pageState;

        public ProjectController(IOptions<DomainOptions> options, IProjectServiceAdaptor adaptor, IPageState pageState)
        {
            this.options = options.Value;
            this.adaptor = adaptor;
            this.pageState = pageState;
        }

        [Route("{title}")]
        public async Task<IActionResult> Project([FromRoute]string title)
        {
            ViewBag.User = User;
            ViewBag.Host = options.Host;
            ProjectView model = await adaptor.Read(title);
            if (model == null)
            {
                return Redirect("/projects");
            }
            model.MetaCollection = pageState.MetaCollection;
            return View(model);
        }
    }
}