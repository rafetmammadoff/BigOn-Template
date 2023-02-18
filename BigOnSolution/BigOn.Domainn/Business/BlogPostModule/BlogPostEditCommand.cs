using BigOn.Domain.AppCode.Extensions;
using BigOn.Domain.Models.DataContexts;
using BigOn.Domain.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BigOn.Domain.Business.BlogPostModule
{
    public class BlogPostEditCommand:IRequest<BlogPost>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int CategoryId { get; set; }
        public string ImagePath { get; set; }
        public IFormFile Image { get; set; }
        public int[] TagIds { get; set; }
        public int modelId { get; set; }

        public class BlogPostEditCommandHandler : IRequestHandler<BlogPostEditCommand, BlogPost>
        {
            private readonly BigOnDbContext db;
            private readonly IHostEnvironment env;

            public BlogPostEditCommandHandler(BigOnDbContext db,IHostEnvironment env)
            {
                this.db = db;
                this.env = env;
            }

            public async Task<BlogPost> Handle(BlogPostEditCommand request, CancellationToken cancellationToken)
            {
                var model=await db.BlogPosts.Include(b=>b.TagCloud).FirstOrDefaultAsync(b=>b.Id==request.Id&&b.DeletedDate==null,cancellationToken);
                if (model==null)
                {
                    return null;
                }
                model.Title= request.Title;
                model.Body= request.Body;
                model.CategoryId= request.CategoryId;
                if (request.Image==null)
                {
                    goto has;
                }

               


                string newImageName = request.Image.GetRandomImagePath("blog");
                await env.SaveAsync(request.Image,newImageName,cancellationToken);
                env.ArchiveImage(model.ImagePath);
                model.ImagePath= newImageName;
                has:

                if (request.TagIds == null)
                {
                    request.TagIds = new int[] { };
                }

                int[] forInsert = request.TagIds.Except(model.TagCloud.Select(x => x.TagId)).ToArray();

                foreach (var item in forInsert)
                {
                    var tc = new BlogPostTagCloud();
                    tc.TagId = item;
                    tc.BlogPostId = model.Id;
                    db.BlogPostTagClouds.Add(tc);
                }


                int[] forDelete = model.TagCloud.Select(x => x.TagId).Except(request.TagIds).ToArray();

                foreach (var item in forDelete)
                {
                    var tc = await db.BlogPostTagClouds.FirstOrDefaultAsync(bptc => bptc.TagId == item && bptc.BlogPostId == model.Id, cancellationToken);
                    db.BlogPostTagClouds.Remove(tc);
                }
                await db.SaveChangesAsync(cancellationToken);
                return model;
            }
        }
    }
}
