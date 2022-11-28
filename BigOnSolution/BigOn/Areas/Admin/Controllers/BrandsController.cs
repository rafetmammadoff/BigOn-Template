using BigOn.Models.DataContexts;
using BigOn.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
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
            var data = _db.Brands.Where(b => b.DeletedDate == null).ToList();
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


        public IActionResult Edit(int id)
        {
            var entity = _db.Brands.FirstOrDefault(b => b.Id == id && b.DeletedDate==null);
                if (entity == null)
                {
                    return NotFound();
                }
                return View(entity);    
        }
        [HttpPost]
        public IActionResult Edit([Bind("Id,Name")] Brand model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var entity = _db.Brands.FirstOrDefault(b => b.Id == model.Id && b.DeletedDate == null);

            if (entity==null)
            {
                return NotFound();
            }
            entity.Name = model.Name;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var entity = _db.Brands.FirstOrDefault(b => b.Id == id && b.DeletedDate == null);
            if (entity == null)
            {
                return NotFound();
            }
            return View(entity);
        }

        [HttpPost]
        public IActionResult Remove(int id)
        {
            
           
            var entity = _db.Brands.FirstOrDefault(b => b.Id == id && b.DeletedDate == null);
            if (entity == null)
            {
                var response = new
                {
                    error = true,
                    message = "Qeyd tapilmadi"
                };
                return Json(response);
            }

            entity.DeletedDate = DateTime.UtcNow.AddHours(4);
            _db.SaveChanges();
            var data = _db.Brands.Where(b => b.DeletedDate == null).ToList();

            return PartialView("_ListBody",data);
        }
    }
}
