using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class CreateSaleHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Sale entities.
    /// The generated Sales will have valid:      
    /// </summary>
    private static readonly Faker<CreateSaleCommand> createSaleHandlerFaker = new Faker<CreateSaleCommand>()
        .RuleFor(u => u.CustomerId, f => f.Random.Guid())
        .RuleFor(u => u.Cancelled, false)
        .RuleFor(u => u.SaleItems,  new List<CreateSaleItemsCommand>() { 
              new CreateSaleItemsCommand() { CodeProduct = "A00100", Quantities = 1, UnitPrices = 10 },
              new CreateSaleItemsCommand() { CodeProduct = "A00101", Quantities = 2, UnitPrices = 20 }} );

    /// <summary>    
    /// The generated Sale will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Sale entity with randomly generated data.</returns>
    public static CreateSaleCommand GenerateValidCommand()
    {
        return createSaleHandlerFaker.Generate();
    }

}
