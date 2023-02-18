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
    public class CategorySingleQuery:IRequest<Category>
    {
        public int Id { get; set; }
        public class CategorySingleQueryHandler : IRequestHandler<CategorySingleQuery, Category>
        {
            private readonly BigOnDbContext _db;

            public CategorySingleQueryHandler(BigOnDbContext db)
            {
                this._db = db;
            }
            public async Task<Category> Handle(CategorySingleQuery request, CancellationToken cancellationToken)
            {
                var data = await _db.Categories.FirstOrDefaultAsync(b => b.Id==request.Id && b.DeletedDate==null);
                return data;
            }
        }
    }
}
