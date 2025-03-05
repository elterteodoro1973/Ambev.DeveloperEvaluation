using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation for ActiveProductSpecification tests
/// to ensure consistency across test cases.
/// </summary>
public static class ActiveProductSpecificationTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Product entities.
    /// The generated Products will have valid:    
    /// - FirstName
    /// - LastName      
    /// </summary>   
    private static readonly Faker<Product> ProductFaker = new Faker<Product>()
        .CustomInstantiator(f => new Product
        {
            Id = f.Random.Guid(),
            Code =  "A00" + f.Random.Int(100, 999).ToString(),            
            Description=  f.Commerce.ProductName(),
            Image= string.Format(@"{0}.jpg", Guid.NewGuid()),
            Category= f.Commerce.Categories(1)[0],
            Price =  f.Random.Decimal(10, 4500),
            QuantityInStock=  f.Random.Short(1, 100),
        });
    /// <summary>
    /// Generates a valid Product entity with the specified status.
    /// </summary>    
    /// <returns>A valid Product entity with randomly generated.</returns>
    public static Product GenerateProduct()
    {
        var Product = ProductFaker.Generate();
        return Product;
    }
}
