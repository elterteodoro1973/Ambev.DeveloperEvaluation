using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Users;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rebus.Bus;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products;

/// <summary>
/// Controller for managing Product operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProductsController : BaseController
{
    private readonly ILogger<UsersController> _logger;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IBus _bus;
    private readonly IProductService _productService;

    /// <summary>
    /// Initializes a new instance of UsersController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public ProductsController(ILogger<UsersController> logger, IMediator mediator, IMapper mapper, IBus bus, IProductService productService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mediator = mediator;
        _mapper = mapper;
        _bus = bus;
        _productService = productService;
    }


    [HttpGet("GetList")]
    [ProducesResponseType(typeof(ApiResponseShortListData<ListProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponseShortData<GetProductResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponseShortData<GetProductResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetList(CancellationToken cancellationToken)
    {
        try
        {
            var response = await _productService.GetAllAsync(cancellationToken);
            return new JsonResult(new { Success = true, Message = "Products retrieved successfully", Data = _mapper.Map<List<ListProductResponse>>(response) });
        }
        catch (Exception e)
        {
            _logger.LogWarning($"An error occurred while retrieving the Products: {e.Message}");
            return BadRequest(new ApiResponseShortData<GetProductResponse>
            {
                Success = false,
                Message = "An error occurred while retrieving the Products: " + e.Message,
                Data = null
            });
        }
    }


    /// <summary>
    /// Retrieves a Product by their name
    /// </summary>
    /// <param name="id">The unique identifier of the Product</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Product details if found</returns>
    [HttpGet("{code}")]
    [ProducesResponseType(typeof(ApiResponseShortData<GetProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponseShortData<GetProductResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponseShortData<GetProductResponse>), StatusCodes.Status404NotFound)]

    public async Task<IActionResult> GetProduct([FromRoute] string code, CancellationToken cancellationToken)
    {
        var request = new GetProductRequest { Code = code };
        var validator = new GetProductRequestValidator();

        try
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponseShortData<GetProductResponse>
                {
                    Success = false,
                    Message = "An error occurred while searching for the Product: " + validationResult.Errors.FirstOrDefault().ErrorMessage.ToString(),
                    Data = null
                });
            }

            var command = _mapper.Map<GetProductCommand>(request.Code);
            var response = await _mediator.Send(command, cancellationToken);

            return new JsonResult(new { Success = true, Message = "Product retrieved successfully", Data = _mapper.Map<GetProductResponse>(response) });
        }
        catch (Exception e)
        {
            _logger.LogWarning($"An error occurred while searching for the Product :{code}");
            return BadRequest(new ApiResponseShortData<GetProductResponse>
            {
                Success = false,
                Message = "An error occurred while searching for the Product: " + e.Message,
                Data = null
            });
        }
    }




    /// <summary>
    /// Creates a new Product
    /// </summary>
    /// <param name="request">The Product creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created Product details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseShortData<CreateProductResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponseShortData<CreateProductResponse>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateProductRequestValidator();

        try
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponseShortData<GetProductResponse>
                {
                    Success = false,
                    Message = "An error occurred while Product created: " + validationResult.Errors.FirstOrDefault().ErrorMessage.ToString(),
                    Data = null
                });
            }

            var command = _mapper.Map<CreateProductCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            await _bus.Advanced.Routing.Send("Ambev", $"Product: {request.Description} created with successfully ");
            _logger.LogWarning($"Product: {request.Description} created with successfully!");

            //int divisor = 0;
            //int resultado = 10 / divisor;

            return Created(string.Empty, new ApiResponseShortData<CreateProductResponse>
            {
                Success = true,
                Message = "Product created successfully",
                Data = _mapper.Map<CreateProductResponse>(response)
            });
        }
        catch (Exception e)
        {
            _logger.LogError($"An error occurred while Product created:: {request.Description}!");
            return BadRequest(new ApiResponseShortData<CreateProductRequest>
            {
                Success = false,
                Message = "An error occurred while Product created: " + e.Message,
                Data = request
            });
        }
    }


    /// <summary>
    /// Deletes a Product by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the Product to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Success response if the Product was deleted</returns>
    [HttpDelete("{code}")]
    [ProducesResponseType(typeof(ApiResponseShort), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponseShort), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponseShort), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProduct([FromRoute] string code, CancellationToken cancellationToken)
    {
        var request = new DeleteProductRequest { Code = code };
        var validator = new DeleteProductRequestValidator();

        try
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);  
            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponseShort
                {
                    Success = false,
                    Message = "An error occurred while  deleted Product: " + validationResult.Errors.FirstOrDefault().ErrorMessage.ToString(),
                });
            }

            var command = _mapper.Map<DeleteProductCommand>(request.Code);
            await _mediator.Send(command, cancellationToken);

            await _bus.Advanced.Routing.Send("Ambev", $"Product deleted nº:{request.Code.ToString()}");
            _logger.LogWarning($"Product deleted nº:{request.Code.ToString()}");

            return new JsonResult(new { Success = true, Message = $"Product code:{request.Code.ToString()}, deleted  with successfully" });

        }
        catch (Exception e)
        {
            _logger.LogWarning($"An error occurred while deleted Product :{request.Code.ToString()} !");
            return BadRequest(new ApiResponseShort
            {
                Success = false,
                Message = "An error occurred while Product deleted: " + e.Message,
            });
        }
    }



}
