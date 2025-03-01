namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Represents the response returned after successfully creating a new Product.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the newly created Product,
/// which can be used for subsequent operations or reference.
/// </remarks>
public class CreateProductResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the newly created Product.
    /// </summary>
    /// <value>A GUID that uniquely identifies the created Product in the system.</value>
    public Guid Id { get; set; }
}
