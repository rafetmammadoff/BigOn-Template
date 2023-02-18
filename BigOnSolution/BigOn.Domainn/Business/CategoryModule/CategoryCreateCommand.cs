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

namespace BigOn.Domain.Business.CategoryModule
{
    public class CategoryCreateCommand:IRequest<Category>
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }

        public class CategoryCreateCommandHandler : IRequestHandler<CategoryCreateCommand, Category>
        {
            private readonly BigOnDbContext command;

            public CategoryCreateCommandHandler(BigOnDbContext command)
            {
                this.command = command;
            }

            public async Task<Category> Handle(CategoryCreateCommand request, CancellationToken cancellationToken)
            {
                var category = new Category
                {
                    Name = request.Name,
                    ParentId= request.ParentId,
                };
                await command.AddAsync(category, cancellationToken);
                await command.SaveChangesAsync(cancellationToken);
                return category;
            }
        }
    }
}
