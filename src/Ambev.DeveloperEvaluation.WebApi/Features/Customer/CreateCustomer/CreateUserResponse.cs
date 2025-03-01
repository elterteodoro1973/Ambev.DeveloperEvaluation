using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers.CreateCustomer;

/// <summary>
/// API response model for CreateCustomer operation
/// </summary>
public class CreateCustomerResponse
{
    /// <summary>
    /// The unique identifier of the created Customer
    /// </summary>
    public Guid Id { get; set; }

    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the role of the Customer.
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    /// Gets or sets the role of the Customer.
    /// </summary>
    public string Email { get; set; }
}
