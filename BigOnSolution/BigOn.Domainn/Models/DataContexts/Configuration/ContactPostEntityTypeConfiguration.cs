using BigOn.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BigOn.Domain.Models.DataContexts.Configuration
{
    public class ContactPostEntityTypeConfiguration : IEntityTypeConfiguration<ContactPost>
    {
        public void Configure(EntityTypeBuilder<ContactPost> builder)
        {
            builder.Property(p=>p.Name).IsRequired();
            builder.Property(p=>p.Email).IsRequired();
            builder.Property(p=>p.Subject).IsRequired();
            builder.Property(p => p.Message).IsRequired();
        }
    }
}
