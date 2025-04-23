using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Validator for CreateSaleCommand that defines validation rules for Sale creation command.
/// </summary>
public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleCommandValidator()
    {
        RuleFor(Sale => Sale.CustomerId).NotEmpty();

        RuleFor(Sale => Sale.SaleItems)
            .Must(saleItems =>
            {
                var groupedItems = saleItems.GroupBy(c => c.CodeProduct)
                    .Select(g => new
                    {
                        CodeProduct = g.Key,
                        TotalQuantities = g.Sum(c => c.Quantities)
                    });

                return groupedItems.All(item => item.TotalQuantities < 20);
            })
            .WithMessage("The total quantities for any product must be less than 20.");
    }
}
