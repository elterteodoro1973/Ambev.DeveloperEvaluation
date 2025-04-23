using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Customers.CreateCustomer;

/// <summary>
/// Validator for CreateCustomerCommand that defines validation rules for Customer creation command.
/// </summary>
public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;

    public CreateCustomerCommandValidator(ICustomerRepository customerRepository)
    {
        ValidorGeral();

        _customerRepository = customerRepository;       

        RuleFor(Customer => Customer.Name)
            .Must(name => !ExistingNameCustomer(name))
            .WithMessage(name => $"Customer with name => '{name}' already exists");

        RuleFor(Customer => Customer.Email)
            .Must(email => !ExistingEmailCustomer(email))
            .WithMessage(email => $"Customer with email '{email}' already exists");       
    }


    public CreateCustomerCommandValidator()
    {
        ValidorGeral();
    }

    private void ValidorGeral()
    {
        RuleFor(Customer => Customer.Email).SetValidator(new EmailValidator());
        RuleFor(Customer => Customer.Name).NotEmpty().Length(3, 50);
        RuleFor(Customer => Customer.Phone).Matches(@"^\+?[1-9]\d{1,14}$");
    }

    /// <summary>
    /// Checks if a customer with the given name already exists.
    /// </summary>
    private bool ExistingNameCustomer(string name)
    {
        var cancellationToken = CancellationToken.None;
        var existingNameCustomer = _customerRepository.GetByNameAsync(name, cancellationToken).Result;
        return existingNameCustomer != null;
    }

    /// <summary>
    /// Checks if a customer with the given email already exists.
    /// </summary>
    private bool ExistingEmailCustomer(string email)
    {
        var cancellationToken = CancellationToken.None;
        var existingEmailCustomer = _customerRepository.GetByEmailAsync(email, cancellationToken).Result;
        return existingEmailCustomer != null;
    }
}
