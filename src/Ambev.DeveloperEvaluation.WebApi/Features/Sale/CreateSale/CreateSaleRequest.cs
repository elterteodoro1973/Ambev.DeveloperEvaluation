using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Represents a request to create a new Sale in the system.
/// </summary>
public class CreateSaleRequest
{
    /// <summary>
    /// The unique CustomerId of the Sale
    /// </summary>
    public Guid? CustomerId { get; set; } = null;

    /// <summary>
    /// The unique cancelled of the Sale
    /// </summary>
    public bool? Cancelled { get; set; } = false;

    /// <summary>
    /// The unique SaleItems of the Sale
    /// </summary>
    public ICollection<CreateSaleItemsRequest> SaleItems { get; set; } = new List<CreateSaleItemsRequest>();    
}
