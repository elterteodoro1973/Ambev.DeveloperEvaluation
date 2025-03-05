using Ambev.DeveloperEvaluation.Application.Customers.CreateCustomer;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class CreateCustomerHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Customer entities.
    /// The generated Customers will have valid:
    /// - Customername (using internet Customernames)    
    /// - Email (valid format)
    /// - Phone (Brazilian format)    
    /// </summary>
    private static readonly Faker<CreateCustomerCommand> createCustomerHandlerFaker = new Faker<CreateCustomerCommand>()
        .RuleFor(u => u.Name, f => f.Name.FirstName()+" "+ f.Name.LastName())        
        .RuleFor(u => u.Email, f => f.Internet.Email())
        .RuleFor(u => u.Phone, f => $"+55{f.Random.Number(11, 99)}{f.Random.Number(100000000, 999999999)}");

    /// <summary>
    /// Generates a valid Customer entity with randomized data.
    /// The generated Customer will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Customer entity with randomly generated data.</returns>
    public static CreateCustomerCommand GenerateValidCommand()
    {
        return createCustomerHandlerFaker.Generate();
    }
}
