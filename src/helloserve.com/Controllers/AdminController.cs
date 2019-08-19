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

            ModelState.Clear(); //need to clear here because the title parameter overrides the Title property through ModelState. https://github.com/aspnet/Mvc/issues/5525
            return View(model);
        }
        
        [HttpPost("admin/blogs")]
        public async Task<IActionResult> Blog([FromForm]BlogCreate model)
        {
            if (ModelState.IsValid)
            {
                await blogServiceAdaptor.Submit(model);
            }

            return View(model);
        }
    }
}