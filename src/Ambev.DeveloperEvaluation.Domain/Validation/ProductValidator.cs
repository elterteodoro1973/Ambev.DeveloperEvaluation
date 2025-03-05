using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        //RuleFor(Product => Product.D).SetValidator(new EmailValidator());

        RuleFor(Product => Product.Code)
            .NotEmpty()
            .MinimumLength(6).WithMessage("Name must be at least 6 characters long.")
            .MaximumLength(10).WithMessage("Name cannot be longer than 10 characters.");

        RuleFor(Product => Product.Description)
            .NotEmpty()
            .MinimumLength(3).WithMessage("Name must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("Name cannot be longer than 50 characters.");        

        RuleFor(Product => Product.Category)
            .NotEmpty()
            .MinimumLength(3).WithMessage("Name must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("Name cannot be longer than 50 characters.");

        RuleFor(Product => Product.Price).NotEmpty();
        RuleFor(Product => Product.QuantityInStock).NotEmpty();

    }
}
