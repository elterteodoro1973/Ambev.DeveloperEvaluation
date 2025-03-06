using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the Product entity class.
/// Tests cover status changes and validation scenarios.
/// </summary>
public class ProductTests
{

    /// <summary>
    /// Tests that validation passes when all Product properties are valid.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid Product data")]
    public void Given_ValidProductData_When_Validated_Then_ShouldReturnValid()
    {
        // Arrange
        var Product = ProductTestData.GenerateValidProduct();

        // Act
        var result = Product.Validate();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    /// <summary>
    /// Tests that validation fails when Product properties are invalid.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid Product data")]
    public void Given_InvalidProductData_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var Product = new Product
        {            
            Description = null,  
            Price = ProductTestData.GenerateValidPrice(), 
            QuantityInStock = 10,                                              
        };

        // Act
        var result = Product.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }
}
