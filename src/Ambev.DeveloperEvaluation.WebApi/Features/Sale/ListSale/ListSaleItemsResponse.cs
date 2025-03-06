using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;

/// <summary>
/// API response model for CreateUser operation
/// </summary>
public class ListSaleItemsResponse
{
    /// <summary>
    /// The unique SaleId of the Sale
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// The unique Quantities of the Sale
    /// </summary>
    public int Quantities { get; set; }

    /// <summary>
    /// The unique UnitPrices of the Sale
    /// </summary>
    public decimal UnitPrices { get; set; }

    /// <summary>
    /// The unique CodeProduct of the Sale
    /// </summary>
    public string CodeProduct { get; set; }

    /// <summary>
    /// The unique CodeProduct of the Sale
    /// </summary>
    public string NameProduct { get; set; }
}
