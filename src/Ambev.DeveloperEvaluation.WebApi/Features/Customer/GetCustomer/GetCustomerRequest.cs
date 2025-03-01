namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers.GetCustomer;

/// <summary>
/// Request model for getting a Customer by ID
/// </summary>
public class GetCustomerRequest
{
    /// <summary>
    /// The unique identifier of the Customer to retrieve
    /// </summary>
    public string Name { get; set; }
}
