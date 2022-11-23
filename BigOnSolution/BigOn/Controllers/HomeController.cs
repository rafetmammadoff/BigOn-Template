using BigOn.Models.DataContexts;
using BigOn.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BigOn.Controllers
{
    public class HomeController : Controller
    {
        private readonly BigOnDbContext _db;

        public HomeController(BigOnDbContext db)
        {
            this._db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(ContactPost model)
        {
            _db.ContactPosts.Add(model);
            _db.SaveChanges();
            TempData["Message"] = "Sorgunuz qebul edildi";
            return RedirectToAction(nameof(Contact));
        }
    }
}
