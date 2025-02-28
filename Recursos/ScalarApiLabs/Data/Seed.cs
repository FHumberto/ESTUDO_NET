using Microsoft.EntityFrameworkCore;

using ScalarApiLabs.Models.Entities;

namespace ScalarApiLabs.Data;

public static class Seed
{
    public static void SeedData(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Product 1", Price = 10.00m },
            new Product { Id = 2, Name = "Product 2", Price = 20.00m },
            new Product { Id = 3, Name = "Product 3", Price = 30.00m }
        );
    }
}
