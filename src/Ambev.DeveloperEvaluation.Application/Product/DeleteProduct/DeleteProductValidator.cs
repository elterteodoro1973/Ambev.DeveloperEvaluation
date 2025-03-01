using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;

/// <summary>
/// Validator for DeleteProductCommand
/// </summary>
public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
{
    /// <summary>
    /// Initializes validation rules for DeleteProductCommand
    /// </summary>
    public DeleteProductValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage("Product ID is required");
    }
}
