using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

/// <summary>
/// Response model for GetProduct operation
/// </summary>
public class GetProductResult
{
    /// <summary>
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
