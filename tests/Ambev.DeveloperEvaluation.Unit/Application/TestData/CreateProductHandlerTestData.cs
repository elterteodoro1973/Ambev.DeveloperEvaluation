using Ambev.DeveloperEvaluation.Application.Customers.CreateCustomer;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class CreateProductHandlerTestData
{    

    /// <summary>
    /// Configures the Faker to generate valid Product entities.
    /// The generated Products will have valid:
    /// - Productname (using internet Productnames) 
    /// </summary> 
    private static readonly Faker<CreateProductCommand> createProductHandlerFaker = new Faker<CreateProductCommand>()
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
    public static CreateProductCommand GenerateValidCommand()
    {
        return createProductHandlerFaker.Generate();
    }
}
