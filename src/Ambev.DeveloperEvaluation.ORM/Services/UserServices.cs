using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;


namespace Ambev.DeveloperEvaluation.ORM.Services;

/// <summary>
/// Handler for processing GetUserCommand requests
/// </summary>
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;    

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    /// <summary>
    /// Handles the GetUserCommand request
    /// </summary>   
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user details if found</returns>
    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync(cancellationToken);
        if (users == null)
            throw new KeyNotFoundException($"Users not found");

        return users;
    }
}
