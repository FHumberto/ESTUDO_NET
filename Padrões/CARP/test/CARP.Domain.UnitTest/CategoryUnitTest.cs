using CARP.Domain.Entities.Categories;

namespace CARP.Domain.UnitTest;

public class CategoryUnitTest
{
    [Fact]
    public void CreateCategory_WithValidParameters_ResultObjectValidState()
    {
        Action action = () => _ = new Category(1, "Category Name");

        action.ShouldNotThrow();
    }

    [Fact]
    public void CreateCategory_NegativeIdValue_DomainExceptionInvalidId()
    {
        Action action = () => _ = new Category(-1, "Category Name");

        var exception = action.ShouldThrow<DomainValidationException>();
        exception.Message.ShouldBe("Invalid Id value");
    }

    [Fact]
    public void CreateCategory_ShortNameValue_DomainExceptionShortName()
    {
        Action action = () => _ = new Category(1, "Ca");
        var exception = action.ShouldThrow<DomainValidationException>();
        exception.Message.ShouldBe(CategoryErrors.CategoryNameTooShort.Description);
    }

    [Fact]
    public void CreateCategory_MissingNameValue_DomainExceptionRequiredName()
    {
        Action action = () => _ = new Category(1, "");
        var exception = action.ShouldThrow<DomainValidationException>();
        exception.Message.ShouldBe(CategoryErrors.CategoryNameIsNullOrEmpty.Description);
    }

    [Fact]
    public void CreateCategory_WithNullNameValue_DomainExceptionInvalidName()
    {
        Action action = () => _ = new Category(1, null);
        action.ShouldThrow<DomainValidationException>();
    }
}