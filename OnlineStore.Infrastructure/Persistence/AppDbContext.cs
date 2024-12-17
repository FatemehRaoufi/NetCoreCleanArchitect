using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities;


namespace OnlineStore.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public static void SeedDatabase(AppDbContext context)
        {
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product { Id = 1, Name = "Laptop", Description = "Gaming Laptop", Price = 1500, Stock = 10 },
                    new Product { Id = 2, Name = "Smartphone", Description = "Flagship Phone", Price = 800, Stock = 20 }
                );
                context.SaveChanges();
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {           
            base.OnModelCreating(modelBuilder); // Always call the base method first

            // Configure the Product entity
            modelBuilder.Entity<Product>()
                .HasKey(e => e.Id);  // Set Id as the primary key

            modelBuilder.Entity<Product>()
                .Property(e => e.Name)
                .HasMaxLength(100); // Set maximum length for the Name property

            modelBuilder.Entity<Product>()
               .Property(e => e.Description)
               .HasMaxLength(100);

            modelBuilder.Entity<Product>()
                .Property(e => e.Price)
                .HasPrecision(18, 2); // Alternate way to set precision and scale in EF Core

            modelBuilder.Entity<Product>()              
              .Property(e => e.Stock)
              .HasPrecision(10, 0);
           
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop", Description = "Gaming Laptop", Price = 1500, Stock = 10 },
                new Product { Id = 2, Name = "Smartphone", Description = "Flagship Phone", Price = 800, Stock = 20 }
            );
        }
    }
}
