using BigOn.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BigOn.Models.DataContexts
{
    public class BigOnDbContext:DbContext
    {
        public BigOnDbContext(DbContextOptions opt):base(opt)
        {

        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<ProductColor> Colors { get; set; }
        public DbSet<ProductSize> Sizes { get; set; }
    }
}
