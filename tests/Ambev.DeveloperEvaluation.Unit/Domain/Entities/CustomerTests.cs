using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the Customer entity class.
/// Tests cover status changes and validation scenarios.
/// </summary>
public class CustomerTests
{
        

    /// <summary>
    /// Tests that validation passes when all Customer properties are valid.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid Customer data")]
    public void Given_ValidCustomerData_When_Validated_Then_ShouldReturnValid()
    {
        // Arrange
        var Customer = CustomerTestData.GenerateValidCustomer();

        // Act
        var result = Customer.Validate();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    /// <summary>
    /// Tests that validation fails when Customer properties are invalid.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid Customer data")]
    public void Given_InvalidCustomerData_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var Customer = new Customer
        {
            Name = "", // Invalid: empty            
            Email = CustomerTestData.GenerateInvalidEmail(), // Invalid: not a valid email
            Phone = CustomerTestData.GenerateInvalidPhone(), // Invalid: doesn't match pattern           
        };

        // Act
        var result = Customer.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }
}
