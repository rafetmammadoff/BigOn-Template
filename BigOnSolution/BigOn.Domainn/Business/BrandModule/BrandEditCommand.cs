using BigOn.Domain.Models.DataContexts;
using BigOn.Domain.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BigOn.Domain.Business.BrandModule
{
    public class BrandEditCommand:IRequest<Brand>
    {
        public int id { get; set; }
        public string Name { get; set; }

        public class BrandEditCommandHandler : IRequestHandler<BrandEditCommand, Brand>
        {
            private readonly BigOnDbContext command;

            public BrandEditCommandHandler(BigOnDbContext command)
            {
                this.command = command;
            }

            public async Task<Brand> Handle(BrandEditCommand request, CancellationToken cancellationToken)
            {
                var brand=await command.Brands.FirstOrDefaultAsync(b=>b.Id==request.id&&b.DeletedDate==null,cancellationToken);
                if (brand==null)
                {
                    return null;
                }
                brand.Name=request.Name;
                await command.SaveChangesAsync();
                return brand;
            }
        }
    }
}
