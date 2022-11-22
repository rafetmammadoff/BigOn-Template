using Microsoft.AspNetCore.Mvc;

namespace BigOn.Controllers
{
    public class BlogPostsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
