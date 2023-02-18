using BigOn.Domain.Business.BlogPostModule;
using BigOn.Domain.Models.DataContexts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BigOn.Controllers
{
    public class BlogController : Controller
    {
        private readonly IMediator mediator;

        public BlogController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<IActionResult> Index(BlogPostAllQuery query)
        {
            var resp=await mediator.Send(query);
            return View(resp);
        }

        [Route("blog/tags/{tagId}")]
        public async Task<IActionResult> PostByTag([FromRoute]BlogPostByTagQuery query)
        {
            var resp = await mediator.Send(query);
            return View("Index",resp);
        }

        [Route("/blog/{slug}")]
        public async Task<IActionResult> Details(BlogPostSingleQuery query)
        {
            var resp = await mediator.Send(query);
            if (resp==null)
            {
                return NotFound();
            }
            return View(resp);
        }
    }
}
