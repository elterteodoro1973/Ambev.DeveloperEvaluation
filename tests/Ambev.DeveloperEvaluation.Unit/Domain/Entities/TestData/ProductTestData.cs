using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;
using Bogus.DataSets;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class ProductTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Product entities.
    /// The generated Products will have valid:
    /// - Productname (using internet Productnames)    
    /// - Email (valid format)
    /// - Phone (Brazilian format)   
    /// </summary>
    private static readonly Faker<Product> ProductFaker = new Faker<Product>()
        .RuleFor(u => u.Code, f => "A00" + f.Random.Int(100, 999).ToString())        
        .RuleFor(u => u.Description, f => f.Commerce.ProductName())
        .RuleFor(u => u.Image, string.Format(@"{0}.jpg", Guid.NewGuid()))
        .RuleFor(u => u.Category, f => f.Commerce.Categories(1)[0])
        .RuleFor(u => u.Price.Value, f => f.Random.Decimal(10, 4500))
        .RuleFor(u => u.QuantityInStock, f => f.Random.Short(1, 100));

    /// <summary>
    /// Generates a valid Product entity with randomized data.
    /// The generated Product will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Product entity with randomly generated data.</returns>
    public static Product GenerateValidProduct()
    {
        return ProductFaker.Generate();
    }

    /// <summary>
    /// Generates a valid email address using Faker.
    /// The generated email will:
    /// - Follow the standard email format (Product@domain.com)
    /// - Have valid characters in both local and domain parts
    /// - Have a valid TLD
    /// </summary>
    /// <returns>A valid email address.</returns>
    public static string GenerateValidEmail()
    {
        return new Faker().Internet.Email();
    }



    /// <summary>
    /// Generates a valid email address using Faker.
    /// The generated email will:
    /// - Follow the standard email format (Product@domain.com)
    /// - Have valid characters in both local and domain parts
    /// - Have a valid TLD
    /// </summary>
    /// <returns>A valid email address.</returns>
    public static string GenerateValidCategory()
    {
        return new Faker().Commerce.Categories(1)[0];
    }


    /// <summary>
    /// Generates a valid email address using Faker.
    /// The generated email will:
    /// - Follow the standard email format (Product@domain.com)
    /// - Have valid characters in both local and domain parts
    /// - Have a valid TLD
    /// </summary>
    /// <returns>A valid email address.</returns>
    public static string GenerateValidDescription()
    {
        return new Faker().Commerce.ProductName();
    }


    /// <summary>
    /// Generates a valid Brazilian phone number.    
    /// </summary>
    /// <returns>A valid Brazilian phone number.</returns>
    public static decimal GenerateValidPrice()
    {
        var faker = new Faker();
        return faker.Random.Number(11, 99);
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
    /// Generates a valid Productname.
    /// The generated Productname will:
    /// - Be between 3 and 50 characters
    /// - Use internet Productname conventions
    /// - Contain only valid characters
    /// </summary>
    /// <returns>A valid Productname.</returns>
    public static string GenerateValidProductname()
    {
        return new Faker().Commerce.ProductName();
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
    /// Generates a Productname that exceeds the maximum length limit.
    /// The generated Productname will:
    /// - Be longer than 50 characters
    /// - Contain random alphanumeric characters
    /// This is useful for testing Productname length validation error cases.
    /// </summary>
    /// <returns>A Productname that exceeds the maximum length limit.</returns>
    public static string GenerateLongProductname()
    {
        return new Faker().Random.String2(51);
    }
}
