using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

/// <summary>
/// Validator for GetProductCommand
/// </summary>
public class GetProductValidator : AbstractValidator<GetProductCommand>
{
    /// <summary>
    /// Initializes validation rules for GetProductCommand
    /// </summary>
    public GetProductValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage("Product Code is required");
    }
}
