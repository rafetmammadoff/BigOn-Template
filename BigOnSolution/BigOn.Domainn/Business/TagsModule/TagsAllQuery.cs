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

namespace BigOn.Domain.Business.TagModule
{
    public class TagsAllQuery:IRequest<List<Tag>>
    {
        public class TagsAllQueryHandler : IRequestHandler<TagsAllQuery, List<Tag>>
        {
            private readonly BigOnDbContext _db;

            public TagsAllQueryHandler(BigOnDbContext db)
            {
                this._db = db;
            }
            public async Task<List<Tag>> Handle(TagsAllQuery request, CancellationToken cancellationToken)
            {
                var data = await _db.Tags.Where(b => b.DeletedDate == null).ToListAsync();
                return data;
            }
        }
    }
}
