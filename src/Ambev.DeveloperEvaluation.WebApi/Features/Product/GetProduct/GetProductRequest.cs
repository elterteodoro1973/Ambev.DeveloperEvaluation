namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

/// <summary>
/// Request model for getting a Product by ID
/// </summary>
public class GetProductRequest
{
    /// <summary>
    /// The unique identifier of the Product to retrieve
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// The unique identifier of the Product to retrieve
    /// </summary>
    public string Code { get; set; }
}
