using CARP.Domain.Abstractions;
using CARP.Domain.Entities.Categories;
using CARP.Domain.Exceptions;

namespace CARP.Domain.Entities.Products;

public sealed class Product : Entity
{
    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public decimal Price { get; private set; }
    public int Stock { get; private set; }
    public string? Image { get; private set; }

    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    public Product(string name, string description, decimal price, int stock, string image)
    {
        ValidateDomain(name, description, price, stock, image);
    }

    public Product(int id, string name, string description, decimal price, int stock, string image)
    {
        DomainValidationException.When(id < 0, "Invalid Id value.");
        Id = id;
        ValidateDomain(name, description, price, stock, image);
    }

    public void Update(string name, string description, decimal price, int stock, string image, int categoryId)
    {
        ValidateDomain(name, description, price, stock, image);
        CategoryId = categoryId;
    }

    private void ValidateDomain(string name, string description, decimal price, int stock, string image)
    {
        DomainValidationException.When(string.IsNullOrEmpty(name),
            ProductErrors.ProductNameIsNullOrEmpty.Description);

        DomainValidationException.When(name.Length < 3,
            ProductErrors.ProductNameTooShort.Description);

        DomainValidationException.When(string.IsNullOrEmpty(description),
            ProductErrors.ProductDescriptionIsNullOrEmpty.Description);

        DomainValidationException.When(description.Length < 5,
            ProductErrors.ProductDescriptionIsTooShort.Description);

        DomainValidationException.When(price < 0,
            ProductErrors.ProductPriceIsInvalid.Description);

        DomainValidationException.When(stock < 0,
            ProductErrors.ProductStockIsInvalid.Description);

        DomainValidationException.When(image?.Length > 250,
            ProductErrors.ProductImageNameTooLong.Description);

        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
        Image = image;
    }
}
