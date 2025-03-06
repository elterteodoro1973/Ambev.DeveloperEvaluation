using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;


namespace Ambev.DeveloperEvaluation.ORM.Services;

/// <summary>
/// Handler for processing GetUserCommand requests
/// </summary>
public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    /// <summary>    
    /// </summary>   
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The customer details if found</returns>
    public async Task<IEnumerable<Customer>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var customers = await _customerRepository.GetAllAsync(cancellationToken);
        if (customers == null)
            throw new KeyNotFoundException($"Customer not found");

        return customers;
    }
}
