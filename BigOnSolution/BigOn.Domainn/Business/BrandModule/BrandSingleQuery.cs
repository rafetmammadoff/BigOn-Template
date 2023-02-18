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

namespace BigOn.Domain.Business.BrandModule
{
    public class BrandSingleQuery:IRequest<Brand>
    {
        public int Id { get; set; }

        public class BrandSingleQueryHandle : IRequestHandler<BrandSingleQuery, Brand>
        {
            private readonly BigOnDbContext db;

            public BrandSingleQueryHandle(BigOnDbContext db)
            {
                this.db = db;
            }
            async Task<Brand> IRequestHandler<BrandSingleQuery, Brand>.Handle(BrandSingleQuery request, CancellationToken cancellationToken)
            {
                var entity = await db.Brands.FirstOrDefaultAsync(b => b.Id == request.Id && b.DeletedDate == null);
                return entity;
            }

        }
    }
}
