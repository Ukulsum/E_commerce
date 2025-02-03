using E_BookWebRazor_Temp.Models;
using Microsoft.EntityFrameworkCore;

namespace E_BookWebRazor_Temp.Data
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", CategoryOrder = 1 },
                new Category { Id = 2, Name = "SciFi", CategoryOrder = 2 },
                new Category { Id = 3, Name = "History", CategoryOrder = 3 }
                );
        }
    }
}
