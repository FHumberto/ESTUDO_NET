namespace CARP.Domain.Entities;

public sealed class Product : Entity
{
    public string? Name { get; private set; }
    public decimal Price { get; private set; }
    public int Stock { get; private set; }
    public string? Image { get; set; }

    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    public Product(string? name, decimal price, int stock, string? image)
    {
        Name = name;
        Price = price;
        Stock = stock;
        Image = image;
    }

    public Product(int id, string? name, decimal price, int stock, string? image)
    {
        base.Id = id;
        Name = name;
        Price = price;
        Stock = stock;
        Image = image;
    }
}
