using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation for ActiveCustomerSpecification tests
/// to ensure consistency across test cases.
/// </summary>
public static class ActiveCustomerSpecificationTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Customer entities.
    /// The generated Customers will have valid:
    /// - Email (valid format)   
    /// - FirstName
    /// - LastName
    /// - Phone (Brazilian format)    
    /// </summary>   
    private static readonly Faker<Customer> CustomerFaker = new Faker<Customer>()
        .CustomInstantiator(f => new Customer
        {
            Id = f.Random.Guid(),
            Email = f.Internet.Email(),          
            Name = f.Name.FirstName() + " " + f.Name.LastName(),
            Phone = $"+55{f.Random.Number(11, 99)}{f.Random.Number(100000000, 999999999)}"           
        });
    /// <summary>
    /// Generates a valid Customer entity with the specified status.
    /// </summary>    
    /// <returns>A valid Customer entity with randomly generated.</returns>
    public static Customer GenerateCustomer()
    {
        var Customer = CustomerFaker.Generate();        
        return Customer;
    }
}
