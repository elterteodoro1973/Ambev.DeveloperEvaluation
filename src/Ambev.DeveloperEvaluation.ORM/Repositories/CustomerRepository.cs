using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Linq;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of ICustomerRepository using Entity Framework Core
/// </summary>
public class CustomerRepository : ICustomerRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of CustomerRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public CustomerRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new Customer in the database
    /// </summary>
    /// <param name="Customer">The Customer to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created Customer</returns>
    public async Task<Customer> CreateAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        await _context.Customer.AddAsync(customer, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return customer;
    }

    /// <summary>
    /// Retrieves a Customer by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the Customer</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Customer if found, null otherwise</returns>
    public async Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Customer.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }   


    /// <summary>
    /// Retrieves a Customer by their unique identifier
    /// </summary>
    /// <param name="name">The unique identifier of the Customer</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Customer if found, null otherwise</returns>
    public async Task<Customer?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Customer.FirstOrDefaultAsync(o => o.Name.ToLower()== name.ToLower(), cancellationToken);
    }

    /// <summary>
    /// Retrieves a Customer by their unique identifier
    /// </summary>
    /// <param name="name">The unique identifier of the Customer</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Customer if found, null otherwise</returns>
    public async Task<Customer?> GetByPartialNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Customer.FirstOrDefaultAsync(o => o.Name.ToLower().Contains(name), cancellationToken);
    }


    /// <summary>
    /// Retrieves a Customer by their email address
    /// </summary>
    /// <param name="email">The email address to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Customer if found, null otherwise</returns>
    public async Task<Customer?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Customer.FirstOrDefaultAsync(u => u.Email == email, cancellationToken); ;
    }

    /// <summary>
    /// Deletes a Customer from the database
    /// </summary>
    /// <param name="id">The unique identifier of the Customer to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the Customer was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var customer = await GetByIdAsync(id, cancellationToken);
        if (customer == null)
            return false;

        _context.Customer.Remove(customer);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    /// <summary>
    /// Retrieves all Customers from the database
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of all Customers</returns>
    public async Task<IEnumerable<Customer>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Customer.ToListAsync(cancellationToken);
    }
    
}
