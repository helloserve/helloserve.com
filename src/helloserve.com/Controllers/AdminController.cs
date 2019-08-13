using helloserve.com.Adaptors;
using helloserve.com.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace helloserve.com.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        readonly IBlogServiceAdaptor blogServiceAdaptor;

        public AdminController(IBlogServiceAdaptor blogServiceAdaptor)
        {
            this.blogServiceAdaptor = blogServiceAdaptor;
        }

        [HttpGet("admin/blogs/{title?}")]
        public async Task<IActionResult> Blog([FromRoute]string title)
        {
            BlogCreate model = new BlogCreate();

            if (!string.IsNullOrEmpty(title))
            {
                model = await blogServiceAdaptor.Edit(title);
            }

            return View(model);
        }
    }
}