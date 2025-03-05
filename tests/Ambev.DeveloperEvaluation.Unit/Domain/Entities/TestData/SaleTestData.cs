using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;
using Castle.Components.DictionaryAdapter;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class SaleTestData
{
    static Guid unique = Guid.NewGuid();

    /// <summary>
    /// Configures the Faker to generate valid Sale entities.
    /// The generated Sales will have valid:      
    /// </summary>
    private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
    .RuleFor(u => u.Id, unique)
    .RuleFor(u => u.CustomerId, f => f.Random.Guid())
    .RuleFor(u => u.Cancelled, false)
    .RuleFor(u => u.SaleItems, f => new List<SaleItems>() {
          new SaleItems() { SaleId = unique, CodeProduct = "A00100", Quantities = 1, UnitPrices = 10 },
          new SaleItems() { SaleId = unique, CodeProduct = "A00101", Quantities = 2, UnitPrices = 20 }});


    /// <summary>
    /// Generates a valid Sale entity with randomized data.   
    /// </summary>
    /// <returns>A valid Sale entity with randomly generated data.</returns>
    public static Sale GenerateValidSale()
    {
        return SaleFaker.Generate();
    }


    /// <summary>    
    /// - Be a simple Guid
    /// This is useful for testing email validation error cases.
    /// </summary>
    /// <returns>An invalid email address.</returns>
    public static Guid GenerateInvalidCustomerId()
    {
        return Guid.NewGuid();
    }


}
