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
    public class BlogPostRecentQuery:IRequest<List<BlogPost>>
    {
        public int Size { get; set; }
        public class BlogPostsAllQueryHandler : IRequestHandler<BlogPostRecentQuery, List<BlogPost>>
        {
            private readonly BigOnDbContext _db;

            public BlogPostsAllQueryHandler(BigOnDbContext db)
            {
                this._db = db;
            }
            public async Task<List<BlogPost>> Handle(BlogPostRecentQuery request, CancellationToken cancellationToken)
            {
                var data = await _db.BlogPosts.Where(b => b.DeletedDate == null).OrderByDescending(b=>b.Id).Take(request.Size <2 ? 2 :request.Size).ToListAsync();
                return data;
            }
        }
    }
}
