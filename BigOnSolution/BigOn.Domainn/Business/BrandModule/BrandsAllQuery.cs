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
    public class BrandsAllQuery:IRequest<List<Brand>>
    {
        public class BrandsAllQueryHandler : IRequestHandler<BrandsAllQuery, List<Brand>>
        {
            private readonly BigOnDbContext _db;

            public BrandsAllQueryHandler(BigOnDbContext db)
            {
                this._db = db;
            }
            public async Task<List<Brand>> Handle(BrandsAllQuery request, CancellationToken cancellationToken)
            {
                var data = await _db.Brands.Where(b => b.DeletedDate == null).ToListAsync();
                return data;
            }
        }
    }
}
