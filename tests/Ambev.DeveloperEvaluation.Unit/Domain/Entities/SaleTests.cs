using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the Sale entity class.
/// Tests cover status changes and validation scenarios.
/// </summary>
public class SaleTests
{
    /// <summary>
    /// Tests that validation passes when all Sale properties are valid.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid Sale data")]
    public void Given_ValidSaleData_When_Validated_Then_ShouldReturnValid()
    {
        // Arrange
        var Sale = SaleTestData.GenerateValidSale();

        // Act
        var result = Sale.Validate();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    /// <summary>
    /// Tests that validation fails when Sale properties are invalid.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid Sale data")]
    public void Given_InvalidSaleData_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var Sale = new Sale
        {                      
            CustomerId = Guid.NewGuid(),
            SaleDate = DateTime.Now.AddDays(-2),
            TotalGrossValue = 0,
            Discounts =0,
            TotalNetValue =0,
            Cancelled = true
        };

        // Act
        var result = Sale.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }
}
