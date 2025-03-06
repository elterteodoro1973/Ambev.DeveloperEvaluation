using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;

/// <summary>
/// API response model for CreateUser operation
/// </summary>
public class ListProductResponse
{
    // <summary>
    /// The unique identifier of the Product
    /// </summary>    
    public Guid Id { get; set; }


    /// <summary>
    /// The unique identifier of the Product
    /// </summary>  
    public string Code { get; set; }


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
