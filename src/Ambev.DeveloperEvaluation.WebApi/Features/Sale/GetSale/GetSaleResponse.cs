using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Features.Sale.GetSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// API response model for GetSale operation
/// </summary>
public class GetSaleResponse
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
    /// The unique CustomerName of the Sale
    /// </summary>
    public string CustomerName { get; set; }

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

    public ICollection<GetSaleItemsResponse> SaleItems { get; set; } = new List<GetSaleItemsResponse>();

}
