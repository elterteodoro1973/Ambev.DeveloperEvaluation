using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Customers.DeleteCustomer;

/// <summary>
/// Handler for processing DeleteCustomerCommand requests
/// </summary>
public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, DeleteCustomerResponse>
{
    private readonly ICustomerRepository _CustomerRepository;

    /// <summary>
    /// Initializes a new instance of DeleteCustomerHandler
    /// </summary>
    /// <param name="CustomerRepository">The Customer repository</param>
    /// <param name="validator">The validator for DeleteCustomerCommand</param>
    public DeleteCustomerHandler(
        ICustomerRepository CustomerRepository)
    {
        _CustomerRepository = CustomerRepository;
    }

    /// <summary>
    /// Handles the DeleteCustomerCommand request
    /// </summary>
    /// <param name="request">The DeleteCustomer command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result of the delete operation</returns>
    public async Task<DeleteCustomerResponse> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteCustomerValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var success = await _CustomerRepository.DeleteAsync(request.Id, cancellationToken);
        if (!success)
            throw new KeyNotFoundException($"Customer with ID {request.Id} not found");

        return new DeleteCustomerResponse { Success = true };
    }
}
