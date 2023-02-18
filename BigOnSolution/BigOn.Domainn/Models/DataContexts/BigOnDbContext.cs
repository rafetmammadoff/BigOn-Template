using BigOn.Domain.Models.Entities;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.EntityFrameworkCore;

namespace BigOn.Domain.Models.DataContexts
{
    public class BigOnDbContext:DbContext
    {
        public BigOnDbContext(DbContextOptions opt):base(opt)
        {

        }

        public DbSet<Subscribe> Subscribers { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ProductColor> Colors { get; set; }
        public DbSet<ProductSize> Sizes { get; set; }
        public DbSet<ProductMaterial> Materials { get; set; }
        public DbSet<ProductType> Types { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ContactPost> ContactPosts { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<BlogPostTagCloud> BlogPostTagClouds { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BlogPostComment> BlogPostComments { get; set; }
        public DbSet<Faq> Faqs { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BigOnDbContext).Assembly);
        }
    }
}
