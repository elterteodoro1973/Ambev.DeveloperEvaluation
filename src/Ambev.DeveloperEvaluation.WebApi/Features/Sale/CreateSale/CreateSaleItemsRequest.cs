using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class  CreateSaleItemsRequest
{
    /// <summary>
    /// The unique SaleItems of the Sale
    /// </summary>
    public string CodeProduct { get; set; } = null;

    /// <summary>
    /// The unique SaleItems of the Sale
    /// </summary>
    public int Quantities { get; set; } = 0;

    /// <summary>
    /// The unique SaleItems of the Sale
    /// </summary>
    public decimal UnitPrices { get; set; } = 0;
}