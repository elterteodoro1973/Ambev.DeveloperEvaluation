using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers.CreateCustomer;

/// <summary>
/// Represents a request to create a new Customer in the system.
/// </summary>
public class CreateCustomerRequest
{
    /// <summary>
    /// Gets or sets the role of the Customer.
    /// </summary>
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