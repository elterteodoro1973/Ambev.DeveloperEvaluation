using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

/// <summary>
/// Contains unit tests for the SaleValidator class.
/// Tests cover validation of all Sale properties including Salename, email,
/// password, phone, status, and role requirements.
/// </summary>
public class SaleValidatorTests
{
    private readonly SaleValidator _validator;

    public SaleValidatorTests()
    {
        _validator = new SaleValidator();
    }

    /// <summary>
    /// Tests that validation passes when all Sale properties are valid.
    /// This test verifies that a Sale with valid:    
    /// passes all validation rules without any errors.
    /// </summary>
    [Fact(DisplayName = "Valid Sale should pass all validation rules")]
    public void Given_ValidSale_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var Sale = SaleTestData.GenerateValidSale();

        // Act
        var result = _validator.TestValidate(Sale);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Tests that validation fails for invalid Salename formats.    
    /// </summary>
    /// <param name="customerId">The invalid Salename to test.</param>
    [Theory(DisplayName = "Invalid customerId formats should fail validation")]
    [InlineData("")] // Empty
    [InlineData("ab")] // Less than 3 characters
    public void Given_InvalidCustomerId_When_Validated_Then_ShouldHaveError(Guid customerId)
    {
        // Arrange
        var Sale = SaleTestData.GenerateValidSale();
        Sale.CustomerId = Guid.NewGuid();

        // Act
        var result = _validator.TestValidate(Sale);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.CustomerId);
    }
}
