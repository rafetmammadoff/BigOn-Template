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
    public class ColorSingleQuery:IRequest<ProductColor>
    {
        public int Id { get; set; }
        public class ColorSingleQueryHandler : IRequestHandler<ColorSingleQuery, ProductColor>
        {
            private readonly BigOnDbContext db;

            public ColorSingleQueryHandler(BigOnDbContext db)
            {
                this.db = db;
            }

            public async Task<ProductColor> Handle(ColorSingleQuery request, CancellationToken cancellationToken)
            {
                var data = await db.Colors.FirstOrDefaultAsync(b => b.Id == request.Id && b.DeletedDate == null);
                return data;
            }
        }
    }
}
