using BigOn.Domain.AppCode.Extensions;
using BigOn.Domain.AppCode.Extentions;
using BigOn.Domain.Models.DataContexts;
using BigOn.Domain.Models.Entities;
using BigOn.Migrations;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BigOn.Domain.Business.BlogPostModule
{
    public class BlogPostCreateCommand:IRequest<BlogPost>
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public int CategoryId { get; set; }
        public IFormFile Image { get; set; }
        public int[] TagIds { get; set; }
        public class BlogPostCreateCommandHandler : IRequestHandler<BlogPostCreateCommand, BlogPost>
        {
            private readonly BigOnDbContext command;
            private readonly IHostEnvironment env;

            public BlogPostCreateCommandHandler(BigOnDbContext command,IHostEnvironment env)
            {
                this.command = command;
                this.env = env;
            }

            public async Task<BlogPost> Handle(BlogPostCreateCommand request, CancellationToken cancellationToken)
            {
                var blogPost = new BlogPost
                {
                    Title = request.Title,
                    Body = request.Body,
                    CategoryId = request.CategoryId,
                };
                blogPost.ImagePath = request.Image.GetRandomImagePath("blog");
                await env.SaveAsync(request.Image, blogPost.ImagePath, cancellationToken);
                blogPost.Slug = blogPost.Title.ToSlug();

                if (request.TagIds != null && request.TagIds.Length>0)
                {
                    blogPost.TagCloud = new List<BlogPostTagCloud>();

                    foreach (var item in request.TagIds.Distinct())
                    {
                        var tc = new BlogPostTagCloud();
                        tc.TagId = item;
                        blogPost.TagCloud.Add(tc);
                    }
                }


                await command.AddAsync(blogPost,cancellationToken);
                await command.SaveChangesAsync(cancellationToken);
                return blogPost;
            }
        }
    }
}
