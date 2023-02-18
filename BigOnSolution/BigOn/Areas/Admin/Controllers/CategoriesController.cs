using BigOn.Domain.Business.CategoryModule;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace BigOn.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly IMediator mediator;

        public CategoriesController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<IActionResult> Index(CategoryTreeQuery query)
        {
            var resp=await mediator.Send(query);
            return View(resp);
        }

        public async Task<IActionResult> Create()
        {
            var resp=await mediator.Send(new CategoryAllQuery());
            ViewBag.ParentId = new SelectList(resp, "Id", "Name",null,"ParentName");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateCommand query)
        {
            var resp= await mediator.Send(query);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(CategorySingleQuery query)
        {
            var data = await mediator.Send(query);
            var resp = await mediator.Send(new CategoryAllQuery());
            ViewBag.ParentId = new SelectList(resp, "Id", "Name", data.ParentId, "ParentName");
            var command = new CategoryEditCommand
            {
                Name = data.Name,
                Id = data.Id,
                ParentId = data.ParentId
            };
            return View(command);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(CategoryEditCommand query)
        {
            var resp = await mediator.Send(query);

            return RedirectToAction(nameof(Index));
        }
    }
}
