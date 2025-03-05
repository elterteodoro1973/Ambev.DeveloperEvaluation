using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Application;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation for ActiveSaleSpecification tests
/// to ensure consistency across test cases.
/// </summary>
public static class ActiveSaleSpecificationTestData
{
    static Guid novoGuid = Guid.NewGuid();

    /// <summary>
    /// Configures the Faker to generate valid Sale entities.
    /// The generated Sales will have valid:
    /// - Email (valid format)   
    /// - FirstName
    /// - LastName
    /// - Phone (Brazilian format)    
    /// </summary>   
    private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
        .CustomInstantiator(f => new Sale
        {
            Id = novoGuid,
            CustomerId = GetLocalCustomerId(),
            SaleDate = DateTime.UtcNow,
            TotalGrossValue = 2000,
            Discounts = 20,
            TotalNetValue = 1800,
            Cancelled = false,
            SaleItems = GetlocalSaleItems(novoGuid, GetLocalCodesProducts())
        });
    /// <summary>
    /// Generates a valid Sale entity with the specified status.
    /// </summary>    
    /// <returns>A valid Sale entity with randomly generated.</returns>
    public static Sale GenerateSale()
    {
        var Sale = SaleFaker.Generate();
        return Sale;
    }

    private static Guid GetLocalCustomerId()
    {
        return CreateSaleHandlerTests.GetCustomerId();
    }

    private static List<string> GetLocalCodesProducts()
    {
        return CreateSaleHandlerTests.GetCodesProducts();
    }

    private static ICollection<SaleItems> GetlocalSaleItems(Guid saleId, List<string> codeProducts)
    {
        return CreateSaleHandlerTests.GetSaleItems(saleId, codeProducts);
    }    
}
