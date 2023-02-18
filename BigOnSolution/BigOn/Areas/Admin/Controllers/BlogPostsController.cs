using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BigOn.Domain.Models.DataContexts;
using BigOn.Domain.Models.Entities;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Extensions.Hosting;
using BigOn.Domain.AppCode.Extensions;
using System.Threading;
using BigOn.Domain.AppCode.Extentions;
using MediatR;
using BigOn.Domain.Business.BlogPostModule;
using BigOn.Domain.Business.CategoryModule;
using BigOn.Domain.Business.TagModule;
using BigOn.Domain.Business.BrandModule;

namespace BigOn.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogPostsController : Controller
    {
        private readonly IHostEnvironment env;
        private readonly IMediator mediator;

        public BlogPostsController(IHostEnvironment env,IMediator mediator)
        {
            this.env = env;
            this.mediator = mediator;
        }

        // GET: Admin/BlogPosts
        public async Task<IActionResult> Index(BlogPostAllQuery query)
        {
            var resp = await mediator.Send(query);
            return View(resp);
        }

        // GET: Admin/BlogPosts/Details/5
        public async Task<IActionResult> Details(BlogPostSingleQuery query)
        {

            var resp = await mediator.Send(query);
            if (resp == null)
            {
                return NotFound();
            }
            var data = await mediator.Send(new CategoryAllQuery());
            ViewBag.CategoryId = new SelectList(data, "Id", "Name", resp.CategoryId);
            var dataTag = await mediator.Send(new TagsAllQuery());
            ViewBag.TagId = new SelectList(dataTag, "Id", "Text");
            var command = new BlogPostEditCommand();
            command.Title = resp.Title;
            command.Body = resp.Body;
            command.CategoryId = resp.CategoryId;
            command.ImagePath = resp.ImagePath;
            command.TagIds = resp.TagCloud.Select(x => x.TagId).ToArray();
            command.modelId = resp.Id;
            Console.WriteLine(command.TagIds);
            return View(command);
            //var resp = await mediator.Send(query);

            //if (resp == null)
            //{
            //    return NotFound();
            //}
            //var data = await mediator.Send(new CategoryAllQuery());
            //ViewBag.CategoryId = new SelectList(data, "Id", "Name", resp.CategoryId);


            //return View(resp);
        }

        // GET: Admin/BlogPosts/Create
        public async Task<IActionResult> Create()
        {
            var data= await mediator.Send(new CategoryAllQuery());
            ViewBag.CategoryId = new SelectList(data, "Id", "Name");

            var dataTag = await mediator.Send(new TagsAllQuery());
            ViewBag.TagId = new SelectList(dataTag, "Id", "Text");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogPostCreateCommand command)
        {
            if (command.Image==null)
            {
                ModelState.AddModelError("Image", "Sekil secilmeyib");
            }


            if (ModelState.IsValid)
            {
                var resp= await mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            var data = await mediator.Send(new CategoryAllQuery());
            ViewBag.CategoryId = new SelectList(data, "Id", "Name", command.CategoryId);

            var dataTag = await mediator.Send(new TagsAllQuery());
            ViewBag.TagId = new SelectList(dataTag, "Id", "Text");
            return View(command);
        }



        public async Task<IActionResult> Edit(BlogPostSingleQuery query)
        {
            var resp=await mediator.Send(query);
            if (resp==null)
            {
                return NotFound() ;
            }
            var data = await mediator.Send(new CategoryAllQuery());
            ViewBag.CategoryId = new SelectList(data, "Id", "Name", resp.CategoryId);
            var dataTag = await mediator.Send(new TagsAllQuery());
            ViewBag.TagId = new SelectList(dataTag, "Id", "Text");
            var command=new BlogPostEditCommand();
            command.Title = resp.Title;
            command.Body= resp.Body;
            command.CategoryId = resp.CategoryId;
            command.ImagePath= resp.ImagePath;
            command.TagIds=resp.TagCloud.Select(x=> x.TagId).ToArray();
            Console.WriteLine(command.TagIds);
            return View(command);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BlogPostEditCommand command)
        {
            var resp = await mediator.Send(command);
            if (resp == null)
            {
                var data = await mediator.Send(new CategoryAllQuery());
                ViewBag.CategoryId = new SelectList(data, "Id", "Name", resp.CategoryId);
            }
            
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> Remove(BlogPostRemoveCommand command)
        {
            var response = await mediator.Send(command);

            if (response.Error)
            {
                return Json(response);
            }
            var data = await mediator.Send(new BlogPostAllQuery());

            return PartialView("_ListBody", data.ToList());
        }



    }
}
