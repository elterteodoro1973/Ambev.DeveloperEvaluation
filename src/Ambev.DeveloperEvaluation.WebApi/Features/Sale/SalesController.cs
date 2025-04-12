using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.ORM.Services;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Users;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rebus.Bus;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale
{
    /// <summary>
    /// Controller for managing Sale operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : Controller
    {       
        private readonly ILogger<UsersController> _logger;        
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IBus _bus;
        private readonly ISaleService _saleService;

        /// <summary>
        /// Initializes a new instance of UsersController
        /// </summary>
        /// <param name="mediator">The mediator instance</param>
        /// <param name="mapper">The AutoMapper instance</param>
        public SalesController(ILogger<UsersController> logger, IMediator mediator, IMapper mapper, IBus bus, ISaleService saleService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator;
            _mapper = mapper;
            _bus = bus;
            _saleService = saleService;
        }

        [HttpGet("GetList")]
        [ProducesResponseType(typeof(ApiResponseShortListData<ListSaleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseShortData<GetSaleResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponseShortData<GetSaleResponse>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetList(CancellationToken cancellationToken)
        {
            try
            {
                var response = await _saleService.GetAllAsync(cancellationToken);
                return new JsonResult(new { Success = true, Message = "Sales retrieved successfully", Data = _mapper.Map<List<ListSaleResponse>>(response) });
            }
            catch (Exception e)
            {
                _logger.LogWarning($"An error occurred while retrieving the Sales: {e.Message}");
                return BadRequest(new ApiResponseShortData<GetSaleResponse>
                {
                    Success = false,
                    Message = "An error occurred while retrieving the Sales: " + e.Message,
                    Data = null
                });
            }
        }


        /// <summary>
        /// Retrieves a Sale by their id
        /// </summary>
        /// <param name="id">The unique identifier of the Sale</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The Sale details if found</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseShortData<GetSaleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseShortData<GetSaleResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponseShortData<GetSaleResponse>), StatusCodes.Status404NotFound)]

        public async Task<IActionResult> GetSale([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new GetSaleRequest { Id = id };
            var validator = new GetSaleRequestValidator();

            try
            {
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return BadRequest(new ApiResponseShortData<GetSaleResponse>
                    {
                        Success = false,
                        Message = "An error occurred while searching for the Sale: " + validationResult.Errors.FirstOrDefault().ErrorMessage.ToString(),
                        Data = null
                    });
                }


                var command = _mapper.Map<GetSaleCommand>(request.Id);
                var response = await _mediator.Send(command, cancellationToken);

                return new JsonResult(new { Success = true, Message = "Sale retrieved successfully", Data = _mapper.Map<GetSaleResponse>(response) });
            }
            catch (Exception e)
            {
                _logger.LogWarning($"An error occurred while searching for the Sale :{id}");
                return BadRequest(new ApiResponseShortData<GetSaleResponse>
                {
                    Success = false,
                    Message = "An error occurred while searching for the Sale: " + e.Message,
                    Data = null
                });
            }
        }




        /// <summary>
        /// Creates a new Sale
        /// </summary>
        /// <param name="request">The Sale creation request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created Sale details</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseShortData<CreateSaleResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponseShortData<CreateSaleResponse>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleRequestValidator();

            try
            {
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return BadRequest(new ApiResponseShortData<GetSaleResponse>
                    {
                        Success = false,
                        Message = "An error occurred while Sale created: " + validationResult.Errors.FirstOrDefault().ErrorMessage.ToString(),
                        Data = null
                    });
                }

                var command = _mapper.Map<CreateSaleCommand>(request);
                var response = await _mediator.Send(command, cancellationToken);

                await _bus.Advanced.Routing.Send("Ambev", $"Sale: {response.Id.ToString()} created with successfully ");
                _logger.LogWarning($"Sale: {response.Id.ToString()} created with successfully!");

                return Created(string.Empty, new ApiResponseShortData<CreateSaleResponse>
                {
                    Success = true,
                    Message = "Sale created successfully",
                    Data = _mapper.Map<CreateSaleResponse>(response)
                });
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occurred while Sale !");
                return BadRequest(new ApiResponseShortData<CreateSaleRequest>
                {
                    Success = false,
                    Message = "An error occurred while Sale created: " + e.Message,
                    Data = request
                });
            }
        }


        /// <summary>
        /// Deletes a Sale by their ID
        /// </summary>
        /// <param name="id">The unique identifier of the Sale to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Success response if the Sale was deleted</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponseShort), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseShort), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponseShort), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSale([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new DeleteSaleRequest { Id = id };
            var validator = new DeleteSaleRequestValidator();

            try
            {
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return BadRequest(new ApiResponseShort
                    {
                        Success = false,
                        Message = "An error occurred while Sale delete: " + validationResult.Errors.FirstOrDefault().ErrorMessage.ToString(),

                    });
                }

                var command = _mapper.Map<DeleteSaleCommand>(request.Id);
                await _mediator.Send(command, cancellationToken);

                await _bus.Advanced.Routing.Send("Ambev", $"Sale deleted nº:{id}");
                _logger.LogWarning($"Sale deleted nº:{id}");

                return new JsonResult(new { Success = true, Message = $"Sale ID nº:{id}, deleted with successfully" });
            }
            catch (Exception e)
            {
                _logger.LogWarning($"An error occurred while deleted Sale:{request.Id.ToString()} !");
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "An error occurred while deleted Sale: " + e.Message,
                });
            }
        }
    }
}