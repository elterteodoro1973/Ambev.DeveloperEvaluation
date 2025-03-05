using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Command for creating a new Product.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for creating a Product, 
/// including Productname, password, phone number, email, status, and role. 
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="CreateProductResult"/>.
/// 
/// The data provided in this command is validated using the 
/// <see cref="CreateProductCommandValidator"/> which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
/// populated and follow the required rules.
/// </remarks>
public class CreateProductCommand : IRequest<CreateProductResult>
{
    /// <summary>
    /// The unique identifier of the Product
    /// </summary>  
    public string Code { get; set; }


    /// <summary>
    /// The unique identifier of the Product
    /// </summary>  
    public string Description { get; set; }

    /// <summary>
    /// The unique identifier of the Product
    /// </summary>  
    public string Category { get; set; }

    /// <summary>
    /// The unique identifier of the Product
    /// </summary>  
    public string Image { get; set; }

    /// <summary>
    /// The unique identifier of the Product
    /// </summary>  
    public decimal? Price { get; set; }

    /// <summary>
    /// The unique identifier of the Product
    /// </summary>  
    public int? QuantityInStock { get; set; }


    public ValidationResultDetail Validate()
    {
        var validator = new CreateProductCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}