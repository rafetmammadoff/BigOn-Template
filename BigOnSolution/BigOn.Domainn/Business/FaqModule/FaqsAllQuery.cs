﻿using BigOn.Domain.Models.DataContexts;
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
    public class FaqsAllQuery:IRequest<List<Faq>>
    {
        public class FaqsAllQueryHandler : IRequestHandler<FaqsAllQuery, List<Faq>>
        {
            private readonly BigOnDbContext _db;

            public FaqsAllQueryHandler(BigOnDbContext db)
            {
                this._db = db;
            }
            public async Task<List<Faq>> Handle(FaqsAllQuery request, CancellationToken cancellationToken)
            {
                var data = await _db.Faqs.Where(b => b.DeletedDate == null).ToListAsync();
                return data;
            }
        }
    }
}
