using BigOn.Domain.Models.DataContexts;
using BigOn.Domain.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BigOn.Domain.Business.BlogPostModule
{
    public class BlogPostSingleQuery:IRequest<BlogPost>
    {
        public int Id { get; set; }
        public string Slug { get; set; }

        public class BlogPostSingleQueryHandle : IRequestHandler<BlogPostSingleQuery, BlogPost>
        {
            private readonly BigOnDbContext db;

            public BlogPostSingleQueryHandle(BigOnDbContext db)
            {
                this.db = db;
            }
            async Task<BlogPost> IRequestHandler<BlogPostSingleQuery, BlogPost>.Handle(BlogPostSingleQuery request, CancellationToken cancellationToken)
            {
                var entity = db.BlogPosts.Include(b=>b.TagCloud).ThenInclude(b=>b.Tag)
                    .Include(b=>b.Category).AsQueryable();

                if (!string.IsNullOrWhiteSpace(request.Slug))
                {
                    return await entity.FirstOrDefaultAsync(b => b.Slug.Equals(request.Slug) && b.DeletedDate == null);
                }
                return await entity.FirstOrDefaultAsync(b => b.Id == request.Id && b.DeletedDate == null);
            }

        }
    }
}
