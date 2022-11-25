using BigOn.Models.DataContexts;
using BigOn.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BigOn.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandsController : Controller
    {
        private readonly BigOnDbContext _db;

        public BrandsController(BigOnDbContext db)
        {
            this._db = db;
        }
        public IActionResult Index()
        {
            var data = _db.Brands.Where(b => b.DeletedDate == null).ToList(); ;
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("Name")]Brand model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _db.Brands.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
