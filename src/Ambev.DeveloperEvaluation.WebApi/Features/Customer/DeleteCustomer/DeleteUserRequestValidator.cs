using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers.DeleteCustomer;

/// <summary>
/// Validator for DeleteCustomerRequest
/// </summary>
public class DeleteCustomerRequestValidator : AbstractValidator<DeleteCustomerRequest>
{
    /// <summary>
    /// Initializes validation rules for DeleteCustomerRequest
    /// </summary>
    public DeleteCustomerRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Customer ID is required");
    }
}
