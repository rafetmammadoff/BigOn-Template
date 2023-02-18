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
    public class ColorEditCommand:IRequest<ProductColor>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string HexCode { get; set; }
        public class ColorEditCommandHandler : IRequestHandler<ColorEditCommand, ProductColor>
        {
            private readonly BigOnDbContext db;

            public ColorEditCommandHandler(BigOnDbContext db)
            {
                this.db = db;
            }

            public async Task<ProductColor> Handle(ColorEditCommand request, CancellationToken cancellationToken)
            {
                var color=await db.Colors.FirstOrDefaultAsync(c=>c.Id==request.Id && c.DeletedDate==null,cancellationToken);
                if (color==null)
                {
                    return null;
                }
                color.Name= request.Name;
                color.HexCode= request.HexCode;
                db.SaveChanges();
                return color;
            }
        }
    }
}
