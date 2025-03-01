using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

/// <summary>
/// Represents a request to create a new Product in the system.
/// </summary>
public class CreateProductRequest
{

    /// <summary>
    /// The unique identifier of the Product
    /// </summary>  
    public string Code { get; set; }


    /// <summary>
    /// The unique identifier of the Product
    /// </summary>  
    public string Title { get; set; }

    /// <summary>
    /// The unique identifier of the Product
    /// </summary>  
    public string Description { get; set; }

    /// <summary>
    /// The unique identifier of the Product
    /// </summary>  
    public string Category { get; set; }

    /// <summary>
    /// The unique identifier of the Product
    /// </summary>  
    public string Image { get; set; }

    /// <summary>
    /// The unique identifier of the Product
    /// </summary>  
    public decimal? Price { get; set; }

    /// <summary>
    /// The unique identifier of the Product
    /// </summary>  
    public int? QuantityInStock { get; set; }
}