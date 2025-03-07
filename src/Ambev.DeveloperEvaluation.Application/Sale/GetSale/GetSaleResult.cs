using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Response model for GetSale operation
/// </summary>
public class GetSaleResult
{
    /// <summary>
    /// The unique identifier of the Sale
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The unique CustomerId of the Sale
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// The unique SaleDate of the Sale
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// The unique TotalGrossValue of the Sale
    /// </summary>
    public decimal? TotalGrossValue { get; set; }

    /// <summary>
    /// The unique Discounts of the Sale
    /// </summary>
    public decimal? Discounts { get; set; }

    /// <summary>
    /// The unique TotalNetValue of the Sale
    /// </summary>
    public decimal? TotalNetValue { get; set; }

    /// <summary>
    /// The unique cancelled of the Sale
    /// </summary>

    public bool? Cancelled { get; set; } = false;

    /// <summary>
    /// The unique Customer of the Sale
    /// </summary>
    public Customer Customer { get; set; }

    /// <summary>
    /// The unique SaleItems of the Sale
    /// </summary>
    public ICollection<GetSaleItemsResult> SaleItems { get; set; } = new List<GetSaleItemsResult>();


}
