using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;

/// <summary>
/// Validator for DeleteProductRequest
/// </summary>
public class DeleteProductRequestValidator : AbstractValidator<DeleteProductRequest>
{
    /// <summary>
    /// Initializes validation rules for DeleteProductRequest
    /// </summary>
    public DeleteProductRequestValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage("Product ID is required");
    }
}
