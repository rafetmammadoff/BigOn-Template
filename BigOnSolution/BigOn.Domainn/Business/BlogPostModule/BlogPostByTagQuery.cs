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
    public class BlogPostByTagQuery:IRequest<List<BlogPost>>
    {
        public int TagId { get; set; }
        public class BlogPostByTagQueryHandler : IRequestHandler<BlogPostByTagQuery, List<BlogPost>>
        {
            private readonly BigOnDbContext _db;

            public BlogPostByTagQueryHandler(BigOnDbContext db)
            {
                this._db = db;
            }
            public async Task<List<BlogPost>> Handle(BlogPostByTagQuery request, CancellationToken cancellationToken)
            {
                //var data = await _db.BlogPosts.Include(b=>b.TagCloud.Where(tc=>tc.TagId==request.TagId))
                //    .Where(m=>m.TagCloud.Any() && m.DeletedDate==null).ToListAsync(cancellationToken);


                var data= await (from bp in _db.BlogPosts
                                 join tc in _db.BlogPostTagClouds on bp.Id equals tc.BlogPostId
                                 where tc.TagId==request.TagId
                                 select bp).Distinct().ToListAsync(cancellationToken);
                                 
                return data;
            }
        }
    }
}
