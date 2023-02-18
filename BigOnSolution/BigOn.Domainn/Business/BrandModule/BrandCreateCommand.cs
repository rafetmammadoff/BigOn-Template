using BigOn.Domain.Models.DataContexts;
using BigOn.Domain.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BigOn.Domain.Business.BrandModule
{
    public class BrandCreateCommand:IRequest<Brand>
    {
        public string Name { get; set; }

        public class BrandCreateCommandHandler : IRequestHandler<BrandCreateCommand, Brand>
        {
            private readonly BigOnDbContext command;

            public BrandCreateCommandHandler(BigOnDbContext command)
            {
                this.command = command;
            }

            public async Task<Brand> Handle(BrandCreateCommand request, CancellationToken cancellationToken)
            {
                var brand = new Brand
                {
                    Name = request.Name
                };
                await command.AddAsync(brand, cancellationToken);
                await command.SaveChangesAsync();
                return brand;
            }
        }
    }
}
