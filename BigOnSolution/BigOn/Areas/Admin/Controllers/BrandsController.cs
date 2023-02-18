using BigOn.Domain.Business.BrandModule;
using BigOn.Domain.Models.DataContexts;
using BigOn.Domain.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BigOn.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandsController : Controller
    {
        private readonly IMediator mediator;

        public BrandsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<IActionResult> Index(BrandsAllQuery query)
        {
            var response=await mediator.Send(query);
            return View(response);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BrandCreateCommand brandCreate)
        {
            var response = await mediator.Send(brandCreate);
            if (response==null)
            {
                return View(brandCreate);
            }
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Edit(BrandSingleQuery query)
        {
                var response= await mediator.Send(query);
                if (response == null)
                {
                    return NotFound();
                }
                return View(response);    
        }
        [HttpPost]
        public async Task<IActionResult> Edit(BrandEditCommand query)
        {
            var response = await mediator.Send(query);

            if (response==null)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(BrandSingleQuery query)
        {
            var response = await mediator.Send(query);
            if (response == null)
            {
                return NotFound();
            }
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Remove(BrandRemoveCommand command)
        {
            var response = await mediator.Send(command);
           
            if (response.Error)
            {
                return Json(response);
            }
            var data = await mediator.Send(new BrandsAllQuery());

            return PartialView("_ListBody",data);
        }
    }
}
