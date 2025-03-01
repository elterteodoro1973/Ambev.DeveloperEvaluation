using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Customers.GetCustomer;

/// <summary>
/// Handler for processing GetCustomerCommand requests
/// </summary>
public class GetCustomerHandler : IRequestHandler<GetCustomerCommand, GetCustomerResult>
{
    private readonly ICustomerRepository _CustomerRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetCustomerHandler
    /// </summary>
    /// <param name="CustomerRepository">The Customer repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for GetCustomerCommand</param>
    public GetCustomerHandler(
        ICustomerRepository CustomerRepository,
        IMapper mapper)
    {
        _CustomerRepository = CustomerRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetCustomerCommand request
    /// </summary>
    /// <param name="request">The GetCustomer command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Customer details if found</returns>
    public async Task<GetCustomerResult> Handle(GetCustomerCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetCustomerValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var Customer = await _CustomerRepository.GetByNameAsync(request.Name, cancellationToken);
        if (Customer == null)
            throw new KeyNotFoundException($"Customer with ID {request.Name} not found");

        return _mapper.Map<GetCustomerResult>(Customer);
    }


}
