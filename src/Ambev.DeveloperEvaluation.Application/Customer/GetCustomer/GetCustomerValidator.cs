using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Customers.GetCustomer;

/// <summary>
/// Validator for GetCustomerCommand
/// </summary>
public class GetCustomerValidator : AbstractValidator<GetCustomerCommand>
{
    /// <summary>
    /// Initializes validation rules for GetCustomerCommand
    /// </summary>
    public GetCustomerValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Customer ID is required");
    }
}
