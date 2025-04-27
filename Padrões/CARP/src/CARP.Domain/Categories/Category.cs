using CARP.Domain.Abstractions;
using CARP.Domain.Products;

namespace CARP.Domain.Categories;

public sealed class Category : Entity
{
    public string? Name { get; private set; }

    public ICollection<Product>? Products { get; set; }

    public Category(string? name)
    {
        Name = name;
    }

    public Category(int id, string? name)
    {
        base.Id = id;
        Name = name;
    }
}
