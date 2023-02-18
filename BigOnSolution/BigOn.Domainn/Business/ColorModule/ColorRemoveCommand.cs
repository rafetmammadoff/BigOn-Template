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

namespace BigOn.Domain.Business.ColorModule
{
    public class ColorRemoveCommand:IRequest<JsonResponse>
    {
        public int Id { get; set; }
        public class ColorRemoveCommandHandler:IRequestHandler<ColorRemoveCommand, JsonResponse>
        {
            private readonly BigOnDbContext db;

            public ColorRemoveCommandHandler(BigOnDbContext db)
            {
                this.db = db;
            }

            public async Task<JsonResponse> Handle(ColorRemoveCommand request, CancellationToken cancellationToken)
            {
                var color = db.Colors.FirstOrDefault(c => c.Id == request.Id && c.DeletedDate == null);
                if (color== null)
                {
                    return new JsonResponse
                    {
                        Error = true,
                        Message = "Qeyd tapilmadi"
                    };
                }
                color.DeletedDate = DateTime.UtcNow.AddHours(4);
                await db.SaveChangesAsync(cancellationToken);
                return new JsonResponse
                {
                    Error = false,
                    Message = "Ugurludur"
                };
            }
        }
    }
}
