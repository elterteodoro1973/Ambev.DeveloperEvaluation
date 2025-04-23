using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Threading;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Validator for CreateProductCommand that defines validation rules for Product creation command.
/// </summary>
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{

    private readonly IProductRepository _productRepository;

    /// <summary>
    /// Initializes a new instance of the CreateProductCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Email: Must be in valid format (using EmailValidator)
    /// - Productname: Required, must be between 3 and 50 characters
    /// - Password: Must meet security requirements (using PasswordValidator)
    /// - Phone: Must match international format (+X XXXXXXXXXX)
    /// - Status: Cannot be set to Unknown
    /// - Role: Cannot be set to None
    /// </remarks>
    public CreateProductCommandValidator(IProductRepository productRepository)
    {
        ValidorGeral();

        _productRepository = productRepository;

        RuleFor(Product => Product.Code)
            .Must(code => !ExistingCodeProduct(code))
            .WithMessage(code => $"Product with Code => '{code}' already exists");

        RuleFor(Product => Product.Description)
            .Must(description => !ExistingDescriptionProduct(description))
            .WithMessage(description => $"Product with Description '{description}' already exists");        
    }


    public CreateProductCommandValidator()
    {
        ValidorGeral();
    }

    private void ValidorGeral()
    {
        RuleFor(Product => Product.Description).NotEmpty().Length(3, 50); ;
        RuleFor(Product => Product.Price).NotEmpty();
        RuleFor(Product => Product.QuantityInStock).NotEmpty();
    }

    /// <summary>
    /// Checks if a Product with the given Code already exists.
    /// </summary>
    private bool ExistingCodeProduct(string code)
    {
        var cancellationToken = CancellationToken.None;
        var existingCodeProduct =  _productRepository.GetByCodeAsync(code, cancellationToken).Result;
        return existingCodeProduct != null;
    }

    /// <summary>
    /// Checks if a Product with the given Description already exists.
    /// </summary>
    private bool ExistingDescriptionProduct(string description)
    {
        var cancellationToken = CancellationToken.None;
        var existingDescriptionProduct = _productRepository.GetByDescriptionAsync(description, cancellationToken).Result;
        return existingDescriptionProduct != null;
    }

}