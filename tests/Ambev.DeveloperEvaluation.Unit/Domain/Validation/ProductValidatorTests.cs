using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;


/// <summary>
/// Contains unit tests for the ProductValidator class.
/// Tests cover validation of all Product properties including Productname, email,
/// password, phone, status, and role requirements.
/// </summary>
public class ProductValidatorTests
{
    private readonly ProductValidator _validator;

    public ProductValidatorTests()
    {
        _validator = new ProductValidator();
    }

    /// <summary>
    /// Tests that validation passes when all Product properties are valid.
    /// This test verifies that a Product with valid:
    /// - Productname (3-50 characters)    
    /// passes all validation rules without any errors.
    /// </summary>
    [Fact(DisplayName = "Valid Product should pass all validation rules")]
    public void Given_ValidProduct_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var Product = ProductTestData.GenerateValidProduct();

        // Act
        var result = _validator.TestValidate(Product);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Tests that validation fails for invalid Productname formats.
    /// This test verifies that Productnames that are:
    /// - Empty strings
    /// - Less than 3 characters
    /// fail validation with appropriate error messages.
    /// The Productname is a required field and must be between 3 and 50 characters.
    /// </summary>
    /// <param name="ProductName">The invalid Productname to test.</param>
    [Theory(DisplayName = "Invalid Productname formats should fail validation")]
    [InlineData("")] // Empty
    [InlineData("ab")] // Less than 3 characters
    public void Given_InvalidProductDescription_When_Validated_Then_ShouldHaveError(string description)
    {
        // Arrange
        var Product = ProductTestData.GenerateValidProduct();
        Product.Description = description;        

        // Act
        var result = _validator.TestValidate(Product);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }

    /// <summary>
    /// Tests that validation fails when Productname exceeds maximum length.
    /// This test verifies that Productnames longer than 50 characters fail validation.
    /// The test uses TestDataGenerator to create a Productname that exceeds the maximum
    /// length limit, ensuring the validation rule is properly enforced.
    /// </summary>
    [Fact(DisplayName = "Productname longer than maximum length should fail validation")]
    public void Given_ProductDescriptionLongerThanMaximum_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var Product = ProductTestData.GenerateValidProduct();
        Product.Description = ProductTestData.GenerateLongProductname();        

        // Act
        var result = _validator.TestValidate(Product);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }


    /// <summary>
    /// Tests that validation fails when Productname exceeds maximum length.
    /// This test verifies that Productnames longer than 50 characters fail validation.
    /// The test uses TestDataGenerator to create a Productname that exceeds the maximum
    /// length limit, ensuring the validation rule is properly enforced.
    /// </summary>
    [Fact(DisplayName = "Category longer than maximum length should fail validation")]
    public void Given_ProductCategoryLongerThanMaximum_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var Product = ProductTestData.GenerateValidProduct();
        Product.Category = ProductTestData.GenerateLongProductname();

        // Act
        var result = _validator.TestValidate(Product);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Category);
    }



    /// <summary>
    /// Tests that validation fails for invalid email formats.
    /// This test verifies that emails that:
    /// - Don't follow the standard email format (Product@domain.com)
    /// - Don't contain @ symbol
    /// - Don't have a valid domain part
    /// fail validation with appropriate error messages.
    /// The test uses TestDataGenerator to create invalid email formats.
    /// </summary>
    [Fact(DisplayName = "Invalid email formats should fail validation")]
    public void Given_InvalidDescription_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var Product = ProductTestData.GenerateValidProduct();
        Product.Description = ProductTestData.GenerateInvalidEmail();

        // Act
        var result = _validator.TestValidate(Product);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }


    /// <summary>
    /// Tests that validation fails for invalid phone formats.
    /// This test verifies that phone numbers that:
    /// - Don't follow the Brazilian phone number format (+55XXXXXXXXXXXX)
    /// - Don't have the correct length
    /// - Don't start with the country code (+55)
    /// fail validation with appropriate error messages.
    /// The test uses TestDataGenerator to create invalid phone number formats.
    /// </summary>
    [Fact(DisplayName = "Invalid phone formats should fail validation")]
    public void Given_InvalidCategory_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var Product = ProductTestData.GenerateValidProduct();
        Product.Category = ProductTestData.GenerateValidCategory();

        // Act
        var result = _validator.TestValidate(Product);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }
}
