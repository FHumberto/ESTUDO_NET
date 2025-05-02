using CARP.Domain.Abstractions;
using CARP.Domain.Entities.Products;
using CARP.Domain.Exceptions;

namespace CARP.Domain.Entities.Categories;

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
        DomainValidationException.When(id < 0, "Invalid Id value");
        Id = id;
        ValidateDomain(name!);
    }

    public void Update(string name)
    {
        ValidateDomain(name);
    }

    private void ValidateDomain(string name)
    {
        DomainValidationException.When(string.IsNullOrEmpty(name),
            CategoryErrors.CategoryNameIsNullOrEmpty.Description);

        DomainValidationException.When(name.Length < 3,
            CategoryErrors.CategoryNameTooShort.Description);

        Name = name;
    }
}
