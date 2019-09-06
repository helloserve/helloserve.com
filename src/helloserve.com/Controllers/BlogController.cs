using helloserve.com.Adaptors;
using helloserve.com.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace helloserve.com.Controllers
{
    [Route("blogs/")]
    public class BlogController : Controller
    {
        readonly DomainOptions options;
        readonly IBlogServiceAdaptor adaptor;
        readonly IPageState pageState;

        public BlogController(IOptions<DomainOptions> options, IBlogServiceAdaptor adaptor, IPageState pageState)
        {
            this.options = options.Value;
            this.adaptor = adaptor;
            this.pageState = pageState;
        }

        [Route("{title}")]
        public async Task<IActionResult> Blog([FromRoute]string title)
        {
            ViewBag.User = User;
            ViewBag.Host = options.Host;
            BlogView model = await adaptor.Read(title);
            if (model == null)
            {
                return Redirect("/blogs");
            }
            model.MetaCollection = pageState.MetaCollection;
            return View(model);
        }
    }
}