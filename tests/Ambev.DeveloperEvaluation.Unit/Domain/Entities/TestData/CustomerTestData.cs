using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class CustomerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Customer entities.
    /// The generated Customers will have valid:
    /// - Customername (using internet Customernames)    
    /// - Email (valid format)
    /// - Phone (Brazilian format)    
    /// </summary>
    private static readonly Faker<Customer> CustomerFaker = new Faker<Customer>()
        .RuleFor(u => u.Name, f => f.Name.FirstName() + " " + f.Name.LastName())        
        .RuleFor(u => u.Email, f => f.Internet.Email())
        .RuleFor(u => u.Phone, f => $"+55{f.Random.Number(11, 99)}{f.Random.Number(100000000, 999999999)}");

    /// <summary>
    /// Generates a valid Customer entity with randomized data.
    /// The generated Customer will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Customer entity with randomly generated data.</returns>
    public static Customer GenerateValidCustomer()
    {
        return CustomerFaker.Generate();
    }

    /// <summary>
    /// Generates a valid email address using Faker.
    /// The generated email will:
    /// - Follow the standard email format (Customer@domain.com)
    /// - Have valid characters in both local and domain parts
    /// - Have a valid TLD
    /// </summary>
    /// <returns>A valid email address.</returns>
    public static string GenerateValidEmail()
    {
        return new Faker().Internet.Email();
    }

    

    /// <summary>
    /// Generates a valid Brazilian phone number.
    /// The generated phone number will:
    /// - Start with country code (+55)
    /// - Have a valid area code (11-99)
    /// - Have 9 digits for the phone number
    /// - Follow the format: +55XXXXXXXXXXXX
    /// </summary>
    /// <returns>A valid Brazilian phone number.</returns>
    public static string GenerateValidPhone()
    {
        var faker = new Faker();
        return $"+55{faker.Random.Number(11, 99)}{faker.Random.Number(100000000, 999999999)}";
    }

    /// <summary>
    /// Generates a valid Customername.
    /// The generated Customername will:
    /// - Be between 3 and 50 characters
    /// - Use internet Customername conventions
    /// - Contain only valid characters
    /// </summary>
    /// <returns>A valid Customername.</returns>
    public static string GenerateValidCustomername()
    {
        return new Faker().Name.FirstName();
    }

    /// <summary>
    /// Generates an invalid email address for testing negative scenarios.
    /// The generated email will:
    /// - Not follow the standard email format
    /// - Not contain the @ symbol
    /// - Be a simple word or string
    /// This is useful for testing email validation error cases.
    /// </summary>
    /// <returns>An invalid email address.</returns>
    public static string GenerateInvalidEmail()
    {
        var faker = new Faker();
        return faker.Lorem.Word();
    }

    /// <summary>
    /// Generates an invalid password for testing negative scenarios.
    /// The generated password will:
    /// - Not meet the minimum length requirement
    /// - Not contain all required character types
    /// This is useful for testing password validation error cases.
    /// </summary>
    /// <returns>An invalid password.</returns>
    public static string GenerateInvalidPassword()
    {
        return new Faker().Lorem.Word();
    }

    /// <summary>
    /// Generates an invalid phone number for testing negative scenarios.
    /// The generated phone number will:
    /// - Not follow the Brazilian phone number format
    /// - Not have the correct length
    /// - Not start with the country code
    /// This is useful for testing phone validation error cases.
    /// </summary>
    /// <returns>An invalid phone number.</returns>
    public static string GenerateInvalidPhone()
    {
        return new Faker().Random.AlphaNumeric(5);
    }

    /// <summary>
    /// Generates a Customername that exceeds the maximum length limit.
    /// The generated Customername will:
    /// - Be longer than 50 characters
    /// - Contain random alphanumeric characters
    /// This is useful for testing Customername length validation error cases.
    /// </summary>
    /// <returns>A Customername that exceeds the maximum length limit.</returns>
    public static string GenerateLongCustomername()
    {
        return new Faker().Random.String2(51);
    }
}
