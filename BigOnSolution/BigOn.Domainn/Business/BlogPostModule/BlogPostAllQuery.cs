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
    public class BlogPostAllQuery:IRequest<List<BlogPost>>
    {
        public class BlogPostsAllQueryHandler : IRequestHandler<BlogPostAllQuery, List<BlogPost>>
        {
            private readonly BigOnDbContext _db;

            public BlogPostsAllQueryHandler(BigOnDbContext db)
            {
                this._db = db;
            }
            public async Task<List<BlogPost>> Handle(BlogPostAllQuery request, CancellationToken cancellationToken)
            {
                var data = await _db.BlogPosts.Where(b => b.DeletedDate == null).ToListAsync();
                return data;
            }
        }
    }
}
