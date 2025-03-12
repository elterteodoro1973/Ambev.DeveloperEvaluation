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
        [ProducesResponseType(typeof(ApiResponseWithData<ListSaleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IEnumerable<ListSaleResponse>> GetList(CancellationToken cancellationToken)
        {
            var response = await _saleService.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<ListSaleResponse>>(response);
        }


        /// <summary>
        /// Creates a new Sale
        /// </summary>
        /// <param name="request">The Sale creation request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created Sale details</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            try
            {
                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                var command = _mapper.Map<CreateSaleCommand>(request);
                var response = await _mediator.Send(command, cancellationToken);

                await _bus.Advanced.Routing.Send("Ambev", $"Sale nº: {response.Id.ToString()} created with successfully ");
                _logger.LogWarning($"Sale nº: {response.Id.ToString()} created with successfully!");

                return Created(string.Empty, new ApiResponseWithData<CreateSaleResponse>
                {
                    Success = true,
                    Message = "Sale created successfully",
                    Data = _mapper.Map<CreateSaleResponse>(response)
                });
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while created of the Sale!");
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "An error occurred while created of the Sale : " + e.Message,
                });
            }
        }

        /// <summary>
        /// Retrieves a Sale by their ID
        /// </summary>
        /// <param name="id">The unique identifier of the Sale</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The Sale details if found</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<GetSaleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSale([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new GetSaleRequest { Id = id };
            var validator = new GetSaleRequestValidator();            

            try
            {
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                var command = _mapper.Map<GetSaleCommand>(request.Id);
                var response = await _mediator.Send(command, cancellationToken);

                return Ok(new ApiResponseWithData<GetSaleResponse>
                {
                    Success = true,
                    Message = "Sale retrieved successfully",
                    Data = _mapper.Map<GetSaleResponse>(response)
                });
            }
            catch (Exception e)
            {
                _logger.LogError("AAn error occurred while searching for the Sale!");
                return BadRequest(new ApiResponseWithData<GetSaleResponse>
                {
                    Success = false,
                    Message = "An error occurred while searching for the Sale: " + e.Message,
                    Data = new GetSaleResponse()
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
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSale([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new DeleteSaleRequest { Id = id };
            var validator = new DeleteSaleRequestValidator();
            
            try
            {
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                var command = _mapper.Map<DeleteSaleCommand>(request.Id);
                await _mediator.Send(command, cancellationToken);

                await _bus.Advanced.Routing.Send("Ambev",$"deleted sale:{request.Id.ToString()}");
                _logger.LogWarning($"Sale:{request.Id.ToString()} deleted with successfully!");

                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = $"Sale nº:{command.Id.ToString()} deleted with successfully"
                });
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
