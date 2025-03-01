using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Command for creating a new Sale.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for creating a Sale, 
/// including Salename, password, phone number, email, status, and role. 
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="CreateSaleResult"/>.
/// 
/// The data provided in this command is validated using the 
/// <see cref="CreateSaleCommandValidator"/> which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
/// populated and follow the required rules.
/// </remarks>
public class CreateSaleItemsCommand 
{
    /// <summary>
    /// The unique SaleItems of the Sale
    /// </summary>
    public string CodeProduct { get; set; } = null;

    /// <summary>
    /// The unique SaleItems of the Sale
    /// </summary>
    public int Quantities { get; set; }=0;

    /// <summary>
    /// The unique SaleItems of the Sale
    /// </summary>
    public decimal UnitPrices { get; set; } = 0;
}
