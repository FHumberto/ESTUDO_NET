using CARP.Domain.Entities.Products;

namespace CARP.Domain.UnitTest;

public class ProductUnitTest1
{
    [Fact]
    public void CreateProduct_WithValidParameters_ResultObjectValidState()
    {
        Action action = () => _ = new Product(1, "Product Name", "Product Description", 9.99m, 99, "product image");
        action.ShouldNotThrow();
    }

    [Fact]
    public void CreateProduct_NegativeIdValue_DomainExceptionInvalidId()
    {
        Action action = () => _ = new Product(-1, "Product Name", "Product Description", 9.99m, 99, "product image");
        action.ShouldThrow<DomainValidationException>().Message.ShouldBe("Invalid Id value.");
    }

    [Fact]
    public void CreateProduct_ShortNameValue_DomainExceptionShortName()
    {
        Action action = () => _ = new Product(1, "Pr", "Product Description", 9.99m, 99, "product image");
        action.ShouldThrow<DomainValidationException>().Message
            .ShouldBe(ProductErrors.ProductNameTooShort.Description);
    }

    [Fact]
    public void CreateProduct_LongImageName_DomainExceptionLongImageName()
    {
        var longImageName = new string('a', 251);
        Action action = () => _ = new Product(1, "Product Name", "Product Description", 9.99m, 99, longImageName);
        action.ShouldThrow<DomainValidationException>()
            .Message.ShouldBe(ProductErrors.ProductImageNameTooLong.Description);
    }

    [Fact]
    public void CreateProduct_WithNullImageName_NoDomainException()
    {
        Action action = () => _ = new Product(1, "Product Name", "cProdut Description", 9.99m, 99, null!);
        action.ShouldNotThrow();
    }

    [Fact]
    public void CreateProduct_WithEmptyImageName_NoDomainException()
    {
        Action action = () => _ = new Product(1, "Product Name", "Product Description", 9.99m, 99, "");
        action.ShouldNotThrow();
    }

    [Fact]
    public void CreateProduct_InvalidPriceValue_DomainException()
    {
        Action action = () => _ = new Product(1, "Product Name", "Product Description", -9.99m, 99, "");
        action.ShouldThrow<DomainValidationException>().Message.ShouldBe(ProductErrors.ProductPriceIsInvalid.Description);
    }

    [Theory]
    [InlineData(-5)] //? o value vai receber o parametro do InlineData
    public void CreateProduct_InvalidStockValue_ExceptionDomainNegativeValue(int value)
    {
        Action action = () => _ = new Product(1, "Pro", "Product Description", 9.99m, value, "product image");
        action.ShouldThrow<DomainValidationException>().Message.ShouldBe(ProductErrors.ProductStockIsInvalid.Description);
    }
}