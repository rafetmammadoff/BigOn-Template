using BigOn.Domain.Models.DataContexts;
using BigOn.Domain.Models.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BigOn.Domain.Business.ColorModule
{
    public class ColorCreateCommand:IRequest<ProductColor>
    {
        public string Name { get; set; }
        public string HexCode { get; set; }
        public class ColorCreateCommandHandler:IRequestHandler<ColorCreateCommand,ProductColor>
        {
            private readonly BigOnDbContext db;

            public ColorCreateCommandHandler(BigOnDbContext db)
            {
                this.db = db;
            }

            public async Task<ProductColor> Handle(ColorCreateCommand request, CancellationToken cancellationToken)
            {
                var color= new ProductColor { Name=request.Name,HexCode=request.HexCode};
                await db.AddAsync(color,cancellationToken);
                await db.SaveChangesAsync();
                return color;
            }
        }
    }
}
