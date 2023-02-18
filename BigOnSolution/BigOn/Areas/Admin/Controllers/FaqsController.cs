using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BigOn.Domain.Models.DataContexts;
using BigOn.Domain.Models.Entities;
using MediatR;
using BigOn.Domain.Business.FaqModule;
using BigOn.Domain.Business.BrandModule;

namespace BigOn.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FaqsController : Controller
    {
        private readonly IMediator mediator;

        public FaqsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET: Admin/Faqs
        public async Task<IActionResult> Index(FaqsAllQuery query)
        {
            var response = await mediator.Send(query);
            return View(response);
        }

        // GET: Admin/Faqs/Details/5
        public async Task<IActionResult> Details(FaqSingleQuery query)
        {
            var response = await mediator.Send(query);

            if (response == null)
            {
                return NotFound();
            }
            if (response == null)
            {
                return NotFound();
            }

            return View(response);
        }

        // GET: Admin/Faqs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Faqs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FaqCreateCommand query)
        {
            var resp = await mediator.Send(query);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(FaqSingleQuery query)
        {
            var resp = await mediator.Send(query);

            if (resp == null)
            {
                return NotFound();
            }

            return View(resp);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,FaqEditCommand query)
        {
            var resp = await mediator.Send(query);
            if (id!=query.Id)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Remove(FaqRemoveCommand command)
        {
            var response = await mediator.Send(command);

            if (response.Error)
            {
                return Json(response);
            }
            var data = await mediator.Send(new FaqsAllQuery());

            return PartialView("_ListBody", data);
        }
    }
}
