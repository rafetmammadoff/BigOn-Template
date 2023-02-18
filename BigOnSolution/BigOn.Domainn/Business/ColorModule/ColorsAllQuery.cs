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

namespace BigOn.Domain.Business.ColorModule
{
    public class ColorsAllQuery:IRequest<List<ProductColor>>
    {
        public class ColorsAllQueryHandler : IRequestHandler<ColorsAllQuery, List<ProductColor>>
        {
            private readonly BigOnDbContext db;

            public ColorsAllQueryHandler(BigOnDbContext db)
            {
                this.db = db;
            }
            public async Task<List<ProductColor>> Handle(ColorsAllQuery request, CancellationToken cancellationToken)
            {
                var data = await db.Colors.Where(c => c.DeletedDate == null).ToListAsync();
                return data;
            }
        }
    }
}
