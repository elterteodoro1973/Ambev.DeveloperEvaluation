using Ambev.DeveloperEvaluation.Application.Customers.DeleteCustomer;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;
using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.DeleteUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rebus.Bus;



namespace Ambev.DeveloperEvaluation.WebApi.Features.Users;

/// <summary>
/// Controller for managing user operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UsersController : BaseController
{
    private readonly ILogger<UsersController> _logger;    
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IBus _bus;
    private readonly IUserService _userService;

    /// <summary>
    /// Initializes a new instance of UsersController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public UsersController(ILogger<UsersController> logger, IMediator mediator, IMapper mapper, IBus bus, IUserService userService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mediator = mediator;
        _mapper = mapper;
        _bus = bus;
        _userService = userService;
    }

    [HttpGet("GetList")]
    [ProducesResponseType(typeof(ApiResponseWithData<ListUserResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetList(CancellationToken cancellationToken)
    {
        try
        {
            var response = await _userService.GetAllAsync(cancellationToken);

            return Ok(new ApiResponseWithData<IEnumerable<ListUserResponse>>
            {
                Success = true,
                Message = "User retrieved successfully",
                Data = _mapper.Map<IEnumerable<ListUserResponse>>(response)
            });
        }
        catch (Exception e)
        {
            _logger.LogError("An error occurred while searching for the users!");
            return BadRequest(new ApiResponseWithData<ListUserResponse>
            {
                Success = false,
                Message = "An error occurred while searching for the users: " + e.Message,
                Data = new ListUserResponse()
            });
        }
    }



    /// <summary>
    /// Creates a new user
    /// </summary>
    /// <param name="request">The user creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created user details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateUserResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var validator = new CreateUserRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateUserCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);
                               
            await _bus.Advanced.Routing.Send("Ambev","User created successfully");
            _logger.LogWarning($"Created user:{request.Name}, with successfully!");

            return Created(string.Empty, new ApiResponseWithData<CreateUserResponse>
            {
                Success = true,
                Message = "User created successfully",
                Data = _mapper.Map<CreateUserResponse>(response)
            });
        }
        catch (Exception e)
        {
            _logger.LogError($"An error occurred while created the user:{request.Name},error:{e.Message}!");
            return BadRequest(new ApiResponse
            {
                Success = false,
                Message = "An error occurred while created the user: " + e.Message,
            });
        }
    }

    /// <summary>
    /// Retrieves a user by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the user</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user details if found</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetUserResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUser([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new GetUserRequest { Id = id };
        var validator = new GetUserRequestValidator();
        
        try
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<GetUserCommand>(request.Id);
            var response = await _mediator.Send(command, cancellationToken);

            return Ok(new ApiResponseWithData<GetUserResponse>
            {
                Success = true,
                Message = "User retrieved successfully",
                Data = _mapper.Map<GetUserResponse>(response)
            });
        }
        catch (Exception e)
        {
            _logger.LogError("An error occurred while searching for the user!");
            return BadRequest(new ApiResponseWithData<GetUserResponse>
            {
                Success = false,
                Message = "An error occurred while searching for the user: " + e.Message,
                Data = new GetUserResponse()
            });
        }


    }

    /// <summary>
    /// Deletes a user by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Success response if the user was deleted</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteUserRequest { Id = id };
        var validator = new DeleteUserRequestValidator();
        
        try
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<DeleteUserCommand>(request.Id);
            await _mediator.Send(command, cancellationToken);
            
            await _bus.Advanced.Routing.Send("Ambev","Delete user");
            _logger.LogWarning("User deleted with successfully!");

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "User deleted with  successfully"
            });
        }
        catch (Exception e)
        {
            _logger.LogError("An error occurred while delete the user!");
            return BadRequest(new ApiResponse
            {
                Success = false,
                Message = "An error occurred while delete the user: " + e.Message,
            });
        }
    }
}
