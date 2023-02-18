using BigOn.Domain.AppCode.Infrastructure;
using BigOn.Domain.Models.DataContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BigOn.Domain.Business.FaqModule
{
    public class FaqRemoveCommand:IRequest<JsonResponse>
    {
        public int Id { get; set; }
        public class FaqRemoveCommandHandler : IRequestHandler<FaqRemoveCommand, JsonResponse>
        {
            private readonly BigOnDbContext command;

            public FaqRemoveCommandHandler(BigOnDbContext command)
            {
                this.command = command;
            }
            public async Task<JsonResponse> Handle(FaqRemoveCommand request, CancellationToken cancellationToken)
            {
                var entity = await command.Faqs.FirstOrDefaultAsync(b => b.Id == request.Id && b.DeletedDate == null,cancellationToken);
                if (entity == null)
                {
                    return new JsonResponse
                    {
                        Error = true,
                        Message = "Qeyd tapilmadi"
                    };
                }

                entity.DeletedDate = DateTime.UtcNow.AddHours(4);
                await command.SaveChangesAsync(cancellationToken);

                return new JsonResponse
                {
                    Error = false,
                    Message = "Success"
                };
            }
        }
    }
}
