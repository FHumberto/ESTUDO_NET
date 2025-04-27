using CARP.Domain.Abstractions;
using CARP.Domain.Categories;

namespace CARP.Domain.Products;

public sealed class Product : Entity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public int Stock { get; private set; }

    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    public Product(string name, string description, decimal price, int stock)
    {
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
    }

    public Product(int id, string name, string description, decimal price, int stock)
    {
        base.Id = id;
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
    }
}
