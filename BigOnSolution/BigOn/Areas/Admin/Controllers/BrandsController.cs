using Microsoft.AspNetCore.Mvc;

namespace BigOn.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
