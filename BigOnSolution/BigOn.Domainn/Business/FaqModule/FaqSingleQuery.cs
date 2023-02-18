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

namespace BigOn.Domain.Business.FaqModule
{
    public class FaqSingleQuery:IRequest<Faq>
    {
        public int Id { get; set; }

        public class FaqSingleQueryHandle : IRequestHandler<FaqSingleQuery, Faq>
        {
            private readonly BigOnDbContext db;

            public FaqSingleQueryHandle(BigOnDbContext db)
            {
                this.db = db;
            }
            async Task<Faq> IRequestHandler<FaqSingleQuery, Faq>.Handle(FaqSingleQuery request, CancellationToken cancellationToken)
            {
                var entity = await db.Faqs.FirstOrDefaultAsync(b => b.Id == request.Id && b.DeletedDate == null);
                return entity;
            }

        }
    }
}
