namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers.DeleteCustomer;

/// <summary>
/// Request model for deleting a Customer
/// </summary>
public class DeleteCustomerRequest
{
    /// <summary>
    /// The unique identifier of the Customer to delete
    /// </summary>
    public Guid Id { get; set; }
}
