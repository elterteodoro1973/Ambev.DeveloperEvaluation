using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services;

/// <summary>
/// Repository interface for User entity operations
/// </summary>
public interface ISaleService
{
    /// <summary>
    /// Select all users in the repository
    /// </summary>    
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created user</returns>
    Task<IEnumerable<Sale>> GetAllAsync(CancellationToken cancellationToken = default);

}

