using Ambev.DeveloperEvaluation.Application.Customers.DeleteCustomer;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;
using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.ORM.Services;
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
    [ProducesResponseType(typeof(ApiResponseShortListData<ListUserResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponseShortData<GetUserResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponseShortData<GetUserResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetList(CancellationToken cancellationToken)
    {
        try
        {
            var response = await _userService.GetAllAsync(cancellationToken);
            return new JsonResult(new { Success = true, Message = "Users retrieved successfully", Data = _mapper.Map<List<ListUserResponse>>(response) });
        }
        catch (Exception e)
        {
            _logger.LogWarning($"An error occurred while retrieving the Users: {e.Message}");
            return BadRequest(new ApiResponseShortData<GetUserResponse>
            {
                Success = false,
                Message = "An error occurred while retrieving the Users: " + e.Message,
                Data = null
            });
        }
    }


    /// <summary>
    /// Retrieves a User by their id
    /// </summary>
    /// <param name="id">The unique identifier of the User</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The User details if found</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseShortData<GetUserResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponseShortData<GetUserResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponseShortData<GetUserResponse>), StatusCodes.Status404NotFound)]
    
    public async Task<IActionResult> GetUser([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new GetUserRequest { Id = id };
        var validator = new GetUserRequestValidator();

        try
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponseShortData<GetUserResponse>
                {
                    Success = false,
                    Message = "An error occurred while searching for the User: " + validationResult.Errors.FirstOrDefault().ErrorMessage.ToString(),
                    Data = null
                });
            }

            
            var command = _mapper.Map<GetUserCommand>(request.Id);
            var response = await _mediator.Send(command, cancellationToken);

            return new JsonResult(new { Success = true, Message = "User retrieved successfully", Data = _mapper.Map<GetUserResponse>(response) });
        }
        catch (Exception e)
        {
            _logger.LogWarning($"An error occurred while searching for the User :{id}");
            return BadRequest(new ApiResponseShortData<GetUserResponse>
            {
                Success = false,
                Message = "An error occurred while searching for the User: " + e.Message,
                Data = null
            });
        }
    }




    /// <summary>
    /// Creates a new User
    /// </summary>
    /// <param name="request">The User creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created User details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseShortData<CreateUserResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponseShortData<CreateUserResponse>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateUserRequestValidator();

        try
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponseShortData<GetUserResponse>
                {
                    Success = false,
                    Message = "An error occurred while User created: " + validationResult.Errors.FirstOrDefault().ErrorMessage.ToString(),
                    Data = null
                });
            }

            var command = _mapper.Map<CreateUserCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            await _bus.Advanced.Routing.Send("Ambev", $"User: {request.Name} created with successfully ");
            _logger.LogWarning($"User: {request.Name} created with successfully!");

            return Created(string.Empty, new ApiResponseShortData<CreateUserResponse>
            {
                Success = true,
                Message = "User created successfully",
                Data = _mapper.Map<CreateUserResponse>(response)
            });
        }
        catch (Exception e)
        {
            _logger.LogError($"An error occurred while User created:: {request.Name}!");
            return BadRequest(new ApiResponseShortData<CreateUserRequest>
            {
                Success = false,
                Message = "An error occurred while User created: " + e.Message,
                Data = request
            });
        }
    }


    /// <summary>
    /// Deletes a User by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the User to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Success response if the User was deleted</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponseShort), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponseShort), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponseShort), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteUserRequest { Id = id };
        var validator = new DeleteUserRequestValidator();

        try
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponseShort
                {
                    Success = false,
                    Message = "An error occurred while User delete: " + validationResult.Errors.FirstOrDefault().ErrorMessage.ToString(),

                });
            }

            var command = _mapper.Map<DeleteUserCommand>(request.Id);
            await _mediator.Send(command, cancellationToken);

            await _bus.Advanced.Routing.Send("Ambev", $"User deleted nº:{id}");
            _logger.LogWarning($"User deleted nº:{id}");

            return new JsonResult(new { Success = true, Message = $"User ID nº:{id}, deleted with successfully" });
        }
        catch (Exception e)
        {
            _logger.LogError($"An error occurred while deleted User ID nº: {id}!");
            return BadRequest(new ApiResponseShort
            {
                Success = false,
                Message = "An error occurred while User deleted: " + e.Message,
            });
        }
    }


}
