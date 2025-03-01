using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers.GetCustomer;

/// <summary>
/// Validator for GetCustomerRequest
/// </summary>
public class GetCustomerRequestValidator : AbstractValidator<GetCustomerRequest>
{
    /// <summary>
    /// Initializes validation rules for GetCustomerRequest
    /// </summary>
    public GetCustomerRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Customer Name is required");
    }
}
