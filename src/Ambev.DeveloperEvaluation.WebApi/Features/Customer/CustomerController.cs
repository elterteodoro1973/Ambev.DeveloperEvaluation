﻿using Ambev.DeveloperEvaluation.Application.Customers.CreateCustomer;
using Ambev.DeveloperEvaluation.Application.Customers.DeleteCustomer;
using Ambev.DeveloperEvaluation.Application.Customers.GetCustomer;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Customers.CreateCustomer;
using Ambev.DeveloperEvaluation.WebApi.Features.Customers.DeleteCustomer;
using Ambev.DeveloperEvaluation.WebApi.Features.Customers.GetCustomer;
using Ambev.DeveloperEvaluation.WebApi.Features.Users;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
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
    private readonly ICustomerService _customerService;

    /// <summary>
    /// Initializes a new instance of UsersController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public CustomersController(ILogger<UsersController> logger, IMediator mediator, IMapper mapper, IBus bus, ICustomerService customerService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mediator = mediator;
        _mapper = mapper;
        _bus = bus;
        _customerService = customerService;
    }

    [HttpGet("GetList")]
    [ProducesResponseType(typeof(ApiResponseShortListData<ListCustomerResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponseShortData<GetCustomerResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponseShortData<GetCustomerResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetList(CancellationToken cancellationToken)
    {
        try
        {
            var response = await _customerService.GetAllAsync(cancellationToken);            
            return new JsonResult(new { Success = true, Message = "Customers retrieved successfully", Data = _mapper.Map<List<ListCustomerResponse>>(response) });
        }
        catch (Exception e)
        {
            _logger.LogWarning($"An error occurred while retrieving the customers: {e.Message}");
            return BadRequest(new ApiResponseShortData<GetCustomerResponse>
            {
                Success = false,
                Message = "An error occurred while retrieving the customers: " + e.Message,
                Data = null
            });
        }
    }


    /// <summary>
    /// Retrieves a Customer by their name
    /// </summary>
    /// <param name="id">The unique identifier of the Customer</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Customer details if found</returns>
    [HttpGet("{name}")]
    [ProducesResponseType(typeof(ApiResponseShortData<GetCustomerResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponseShortData<GetCustomerResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponseShortData<GetCustomerResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCustomer([FromRoute] String name, CancellationToken cancellationToken)
    {
        var request = new GetCustomerRequest { Name = name };
        var validator = new GetCustomerRequestValidator();

        try
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
                        
            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponseShortData<GetCustomerResponse>
                {
                    Success = false,
                    Message = "An error occurred while searching for the customer: " + validationResult.Errors.FirstOrDefault().ErrorMessage.ToString(),
                    Data = null
                });
            }


            var command = _mapper.Map<GetCustomerCommand>(request.Name);
            var response = await _mediator.Send(command, cancellationToken);

            return new JsonResult(new { Success = true, Message = "Customer retrieved successfully", Data = _mapper.Map<GetCustomerResponse>(response) });
        }
        catch (Exception e)
        {
            _logger.LogWarning($"An error occurred while searching for the customer :{name}");
            return BadRequest(new ApiResponseShortData<GetCustomerResponse>
            {
                Success = false,
                Message = "An error occurred while searching for the customer: " + e.Message,
                Data = null
            });
        }
    }




    /// <summary>
    /// Creates a new Customer
    /// </summary>
    /// <param name="request">The Customer creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created Customer details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseShortData<CreateCustomerResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponseShortData<CreateCustomerResponse>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateCustomerRequestValidator();       

        try
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponseShortData<GetCustomerResponse>
                {
                    Success = false,
                    Message = "An error occurred while Customer created: " + validationResult.Errors.FirstOrDefault().ErrorMessage.ToString(),
                    Data = null
                });
            }
                
            var command = _mapper.Map<CreateCustomerCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            await _bus.Advanced.Routing.Send("Ambev",$"Customer: {request.Name} created with successfully ");
            _logger.LogWarning($"Customer: {request.Name} created with successfully!");

            return Created(string.Empty, new ApiResponseShortData<CreateCustomerResponse>
            {
                Success = true,
                Message = "Customer created successfully",
                Data = _mapper.Map<CreateCustomerResponse>(response)
            });
        }
        catch (Exception e)
        {
            _logger.LogError($"An error occurred while Customer created:: {request.Name}!");
            return BadRequest(new ApiResponseShortData<CreateCustomerRequest>
            {
                Success = false,
                Message = "An error occurred while Customer created: " + e.Message,
                Data = request
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
    [ProducesResponseType(typeof(ApiResponseShort), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponseShort), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponseShort), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCustomer([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteCustomerRequest { Id = id };
        var validator = new DeleteCustomerRequestValidator(); 

        try
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
                        
            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponseShort
                {
                    Success = false,
                    Message = "An error occurred while Customer deleted: " + validationResult.Errors.FirstOrDefault().ErrorMessage.ToString(),
                    
                });
            }

            var command = _mapper.Map<DeleteCustomerCommand>(request.Id);
            await _mediator.Send(command, cancellationToken);

            await _bus.Advanced.Routing.Send("Ambev",$"Customer deleted nº:{id}");
            _logger.LogWarning($"Customer deleted nº:{id}");

            return new JsonResult(new { Success = true, Message = $"Customer ID nº:{id}, deleted with successfully" });
        }
        catch (Exception e)
        {
            _logger.LogError($"An error occurred while Customer deleted: {id}!");
            return BadRequest(new ApiResponseShort
            {
                Success = false,
                Message = "An error occurred while  deleted Customer: " + e.Message,
            });
        }
    }
}
