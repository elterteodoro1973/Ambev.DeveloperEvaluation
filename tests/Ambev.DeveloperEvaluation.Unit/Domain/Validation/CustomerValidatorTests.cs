using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;


/// <summary>
/// Contains unit tests for the CustomerValidator class.
/// Tests cover validation of all Customer properties including Customername, email,
/// password, phone, status, and role requirements.
/// </summary>
public class CustomerValidatorTests
{
    private readonly CustomerValidator _validator;

    public CustomerValidatorTests()
    {
        _validator = new CustomerValidator();
    }

    /// <summary>
    /// Tests that validation passes when all Customer properties are valid.
    /// This test verifies that a Customer with valid:
    /// - Customername (3-50 characters)    
    /// - Email (valid format)
    /// - Phone (valid Brazilian format)    
    /// passes all validation rules without any errors.
    /// </summary>
    [Fact(DisplayName = "Valid Customer should pass all validation rules")]
    public void Given_ValidCustomer_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var Customer = CustomerTestData.GenerateValidCustomer();

        // Act
        var result = _validator.TestValidate(Customer);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Tests that validation fails for invalid Customername formats.
    /// This test verifies that Customernames that are:
    /// - Empty strings
    /// - Less than 3 characters
    /// fail validation with appropriate error messages.
    /// The Customername is a required field and must be between 3 and 50 characters.
    /// </summary>
    /// <param name="CustomerName">The invalid Customername to test.</param>
    [Theory(DisplayName = "Invalid Customername formats should fail validation")]
    [InlineData("")] // Empty
    [InlineData("ab")] // Less than 3 characters
    public void Given_InvalidCustomername_When_Validated_Then_ShouldHaveError(string name)
    {
        // Arrange
        var Customer = CustomerTestData.GenerateValidCustomer();
        Customer.Name = name;

        // Act
        var result = _validator.TestValidate(Customer);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    /// <summary>
    /// Tests that validation fails when Customername exceeds maximum length.
    /// This test verifies that Customernames longer than 50 characters fail validation.
    /// The test uses TestDataGenerator to create a Customername that exceeds the maximum
    /// length limit, ensuring the validation rule is properly enforced.
    /// </summary>
    [Fact(DisplayName = "Customername longer than maximum length should fail validation")]
    public void Given_CustomerNameLongerThanMaximum_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var Customer = CustomerTestData.GenerateValidCustomer();
        Customer.Name = CustomerTestData.GenerateLongCustomername();
        
        // Act
        var result = _validator.TestValidate(Customer);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    /// <summary>
    /// Tests that validation fails for invalid email formats.
    /// This test verifies that emails that:
    /// - Don't follow the standard email format (Customer@domain.com)
    /// - Don't contain @ symbol
    /// - Don't have a valid domain part
    /// fail validation with appropriate error messages.
    /// The test uses TestDataGenerator to create invalid email formats.
    /// </summary>
    [Fact(DisplayName = "Invalid email formats should fail validation")]
    public void Given_InvalidEmail_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var Customer = CustomerTestData.GenerateValidCustomer();
        Customer.Email = CustomerTestData.GenerateInvalidEmail();

        // Act
        var result = _validator.TestValidate(Customer);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
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
    public void Given_InvalidPhone_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var Customer = CustomerTestData.GenerateValidCustomer();
        Customer.Phone = CustomerTestData.GenerateInvalidPhone();

        // Act
        var result = _validator.TestValidate(Customer);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Phone);
    }    
}
