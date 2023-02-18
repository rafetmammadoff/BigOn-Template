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

namespace BigOn.Domain.Business.CategoryModule
{
    public class CategoryAllQuery:IRequest<List<Category>>
    {
        public class CategoryAllQueryHandler : IRequestHandler<CategoryAllQuery, List<Category>>
        {
            private readonly BigOnDbContext _db;

            public CategoryAllQueryHandler(BigOnDbContext db)
            {
                this._db = db;
            }
            public async Task<List<Category>> Handle(CategoryAllQuery request, CancellationToken cancellationToken)
            {
                var data = await _db.Categories.Include(c=>c.Parent).Where(b => b.DeletedDate == null).ToListAsync();
                return data;
            }
        }
    }
}
