﻿using BigOn.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Domain.Models.DataContexts.Configuration
{
    public class BlogPostTagCloudEntityTypeConfiguration : IEntityTypeConfiguration<BlogPostTagCloud>
    {
        public void Configure(EntityTypeBuilder<BlogPostTagCloud> builder)
        {
            builder.HasKey(x => new
            {
                x.BlogPostId,
                x.TagId
            });
            
            builder.ToTable("BlogPostTagCloud");
        }
    }
}
