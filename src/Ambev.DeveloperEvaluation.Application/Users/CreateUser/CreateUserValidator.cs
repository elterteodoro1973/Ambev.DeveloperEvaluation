using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Threading;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser;

/// <summary>
/// Validator for CreateUserCommand that defines validation rules for user creation command.
/// </summary>
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{

    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Initializes a new instance of the CreateUserCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Email: Must be in valid format (using EmailValidator)
    /// - Username: Required, must be between 3 and 50 characters
    /// - Password: Must meet security requirements (using PasswordValidator)
    /// - Phone: Must match international format (+X XXXXXXXXXX)
    /// - Status: Cannot be set to Unknown
    /// - Role: Cannot be set to None
    /// </remarks>
    public CreateUserCommandValidator(IUserRepository userRepository)
    {
        ValidorGeral();

        _userRepository = userRepository;

        RuleFor(user => user.Email)
            .Must(email => !ExistingUser(email))
            .WithMessage(email => $"User with email=> {email} already exists");

        RuleFor(user => user.Name)
            .Must(name => !ExistingName(name))
            .WithMessage(name => $"User with Name => {name} already exists");        
    }

    public CreateUserCommandValidator()
    {
        ValidorGeral();
    }

    private void ValidorGeral()
    {
        RuleFor(user => user.Email).SetValidator(new EmailValidator());
        RuleFor(user => user.Name).NotEmpty().Length(13, 50);
        RuleFor(user => user.Password).SetValidator(new PasswordValidator());
        RuleFor(user => user.Phone).Matches(@"^\+?[1-9]\d{1,14}$");
        RuleFor(user => user.Status).NotEqual(UserStatus.Unknown.ToString());
        RuleFor(user => user.Role).NotEqual(EnumUserRole.None.ToString());
    }


    /// <summary>
    /// Checks if a Product with the given Code already exists.
    /// </summary>
    private bool ExistingUser(string email)
    {
        var cancellationToken = CancellationToken.None;
        var existingUser = _userRepository.GetByEmailAsync(email, cancellationToken).Result; ;
        return existingUser != null;
    }

    /// <summary>
    /// Checks if a Product with the given Description already exists.
    /// </summary>
    private bool ExistingName(string name)
    {
        var cancellationToken = CancellationToken.None;
        var existingDescriptionProduct = _userRepository.GetByNameAsync(name, cancellationToken).Result;
        return existingDescriptionProduct != null;
    }


}