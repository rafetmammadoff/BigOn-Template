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

namespace BigOn.Domain.Business.CategoryModule
{
    public class CategoryEditCommand:IRequest<Category>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }

        public class CategoryEditCommandHandler : IRequestHandler<CategoryEditCommand, Category>
        {
            private readonly BigOnDbContext command;

            public CategoryEditCommandHandler(BigOnDbContext command)
            {
                this.command = command;
            }

            public async Task<Category> Handle(CategoryEditCommand request, CancellationToken cancellationToken)
            {
                var category = await command.Categories.FirstOrDefaultAsync(c => c.Id == request.Id && c.DeletedDate == null, cancellationToken);
                category.Name= request.Name;
                category.ParentId= request.ParentId;
                await command.SaveChangesAsync(cancellationToken);
                return category;
            }
        }
    }
}
