using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SaleValidator : AbstractValidator<Sale>
{
    public SaleValidator()
    {
        //RuleFor(Sale => Sale.D).SetValidator(new EmailValidator());
        RuleFor(Sale => Sale.CustomerId).NotEmpty();

        RuleFor(Sale => Sale.SaleDate).NotEmpty();

        RuleFor(Sale => Sale.TotalGrossValue).NotNull().NotEqual(0);

        RuleFor(Sale => Sale.TotalNetValue).NotNull().NotEqual(0);

        RuleFor(Sale => Sale.Discounts).NotEmpty();

        RuleFor(Sale => Sale.SaleItems).NotEmpty();
    }
}
