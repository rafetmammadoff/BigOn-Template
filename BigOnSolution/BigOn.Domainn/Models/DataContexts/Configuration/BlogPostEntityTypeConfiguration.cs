using BigOn.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BigOn.Domain.Models.DataContexts.Configuration
{
    public class BlogPostEntityTypeConfiguration : IEntityTypeConfiguration<BlogPost>
    {
        public void Configure(EntityTypeBuilder<BlogPost> builder)
        {
            builder.Property(p=>p.Title).IsRequired();
            builder.Property(p=>p.Slug).IsRequired();
            builder.HasIndex(p=>p.Slug).IsUnique();
            builder.Property(p=>p.Body).IsRequired();
            builder.Property(p=>p.ImagePath).IsRequired();
        }
    }
}
