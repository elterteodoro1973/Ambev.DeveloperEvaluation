namespace Ambev.DeveloperEvaluation.Application.Customers.CreateCustomer;

/// <summary>
/// Represents the response returned after successfully creating a new Customer.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the newly created Customer,
/// which can be used for subsequent operations or reference.
/// </remarks>
public class CreateCustomerResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the newly created Customer.
    /// </summary>
    /// <value>A GUID that uniquely identifies the created Customer in the system.</value>
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
