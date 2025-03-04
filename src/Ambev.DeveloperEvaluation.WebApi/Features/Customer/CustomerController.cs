using Ambev.DeveloperEvaluation.Application.Customers.CreateCustomer;
using Ambev.DeveloperEvaluation.Application.Customers.DeleteCustomer;
using Ambev.DeveloperEvaluation.Application.Customers.GetCustomer;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Customers.CreateCustomer;
using Ambev.DeveloperEvaluation.WebApi.Features.Customers.DeleteCustomer;
using Ambev.DeveloperEvaluation.WebApi.Features.Customers.GetCustomer;
using Ambev.DeveloperEvaluation.WebApi.Features.Users;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rebus.Bus;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers;

/// <summary>
/// Controller for managing Customer operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CustomersController : BaseController
{
    private readonly ILogger<UsersController> _logger;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IBus _bus;
    /// <summary>
    /// Initializes a new instance of CustomersController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public CustomersController(ILogger<UsersController> logger, IMediator mediator, IMapper mapper, IBus bus)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mediator = mediator;
        _mapper = mapper;
        _bus = bus;
    }

    /// <summary>
    /// Creates a new Customer
    /// </summary>
    /// <param name="request">The Customer creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created Customer details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateCustomerResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateCustomerRequestValidator();       

        try
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateCustomerCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            await _bus.Publish($"Customer: {request.Name} created with successfully ");
            _logger.LogWarning($"Customer: {request.Name} created with successfully!");

            return Created(string.Empty, new ApiResponseWithData<CreateCustomerResponse>
            {
                Success = true,
                Message = "Customer created successfully",
                Data = _mapper.Map<CreateCustomerResponse>(response)
            });
        }
        catch (Exception e)
        {
            _logger.LogError($"An error occurred while Customer created:: {request.Name}!");
            return BadRequest(new ApiResponse
            {
                Success = false,
                Message = "An error occurred while Customer created: " + e.Message,
            });
        }
    }

    /// <summary>
    /// Retrieves a Customer by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the Customer</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Customer details if found</returns>
    [HttpGet("{name}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetCustomerResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCustomer([FromRoute] String name, CancellationToken cancellationToken)
    {
        var request = new GetCustomerRequest { Name = name };
        var validator = new GetCustomerRequestValidator();        

        try
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<GetCustomerCommand>(request.Name);
            var response = await _mediator.Send(command, cancellationToken);

            return Ok(new ApiResponseWithData<GetCustomerResponse>
            {
                Success = true,
                Message = "Customer retrieved successfully",
                Data = _mapper.Map<GetCustomerResponse>(response)
            });
        }
        catch (Exception e)
        {
            return BadRequest(new ApiResponse
            {
                Success = false,
                Message = "An error occurred while Customer retrieved: " + e.Message,
            });
        }
    }

    /// <summary>
    /// Deletes a Customer by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the Customer to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Success response if the Customer was deleted</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCustomer([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteCustomerRequest { Id = id };
        var validator = new DeleteCustomerRequestValidator(); 

        try
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<DeleteCustomerCommand>(request.Id);
            await _mediator.Send(command, cancellationToken);

            await _bus.Publish($"Customer deleted nº:{id}");
            _logger.LogWarning($"Customer deleted nº:{id}");

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Customer deleted successfully"
            });
        }
        catch (Exception e)
        {
            _logger.LogError($"An error occurred while Customer deleted: {id}!");
            return BadRequest(new ApiResponse
            {
                Success = false,
                Message = "An error occurred while Customer deleted: " + e.Message,
            });
        }
    }
}
