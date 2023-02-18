using BigOn.Domain.Business.BrandModule;
using BigOn.Domain.Business.ColorModule;
using BigOn.Domain.Models.DataContexts;
using BigOn.Domain.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BigOn.Areas.Admin.Controllers
{
    
    [Area("Admin")]
    public class ColorsController : Controller
    {
        private readonly BigOnDbContext _db;
        private readonly IMediator mediator;

        public ColorsController(BigOnDbContext db,IMediator mediator)
        {
            this._db = db;
            this.mediator = mediator;
        }
        public async Task<IActionResult> Index(ColorsAllQuery query)
        {
            var response=await mediator.Send(query);
            return View(response);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ColorCreateCommand query)
        {
            var resp= await mediator.Send(query);

            if (resp==null)
            {
                return View(query);
            }
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Edit(ColorSingleQuery query)
        {
            var entity = await mediator.Send(query);
            if (entity == null)
            {
                return NotFound();
            }
            return View(entity);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ColorEditCommand query)
        {

            var entity = await mediator.Send(query);
            if (entity == null)
            {
                return NotFound();
            }
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Details(ColorSingleQuery query)
        {
            var resp=await mediator.Send(query);  
            if (resp == null)
            {
                return NotFound();
            }
            return View(resp);
        }

        [HttpPost]
        public async Task<IActionResult> Remove(ColorRemoveCommand query)
        {
            var resp = await mediator.Send(query);
            if (resp.Error)
            {
                return Json(resp);
            }
            var data = await mediator.Send(new ColorsAllQuery());
            return PartialView("_ColorListBody", data);

        }
    }
}
