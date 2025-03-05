using Ambev.DeveloperEvaluation.Application.Customers.CreateCustomer;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contains unit tests for the <see cref="CreateSaleHandler"/> class.
/// </summary>
public class CreateSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly CreateSaleHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleHandlerTests"/> class.
    /// Sets up the test dependencies and creates fake data generators.
    /// </summary>
    public CreateSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _customerRepository = Substitute.For<ICustomerRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateSaleHandler(_saleRepository, _mapper);
    }

    /// <summary>
    /// Tests that a valid Sale creation request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid Sale data When creating Sale Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var id = Guid.NewGuid();
        var customerId = GetCustomerId();
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        var Sale = new Sale
        {
            Id = id,
            CustomerId = customerId,
            SaleDate = DateTime.UtcNow,
            TotalGrossValue = 0,
            Discounts = 0,
            TotalNetValue = 0,
            Cancelled = false,
            SaleItems = GetSaleItems(customerId, GetCodesProducts()),
        };

        var result = new CreateSaleResult
        {
            Id = Sale.Id,
        };

        _mapper.Map<Sale>(command).Returns(Sale);
        _mapper.Map<CreateSaleResult>(Sale).Returns(result);

        _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
            .Returns(Sale);        

        // When
        var createSaleResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createSaleResult.Should().NotBeNull();
        createSaleResult.Id.Should().Be(Sale.Id);
        await _saleRepository.Received(1).CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid Sale creation request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid Sale data When creating Sale Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new CreateSaleCommand(); // Empty command will fail validation

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }   

    /// <summary>
    /// Tests that the mapper is called with the correct command.
    /// </summary>
    [Fact(DisplayName = "Given valid command When handling Then maps command to Sale entity")]
    public async Task Handle_ValidRequest_MapsCommandToSale()
    {
        var id = Guid.NewGuid();
        var customerId = GetCustomerId();
        // Given
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        var Sale = new Sale
        {
            Id = id,
            CustomerId = customerId,
            SaleDate = DateTime.UtcNow,
            TotalGrossValue = 0,
            Discounts = 0,
            TotalNetValue = 0,
            Cancelled = false,
            SaleItems = GetSaleItems(customerId,GetCodesProducts()),
        };

        _mapper.Map<Sale>(command).Returns(Sale);
        _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
            .Returns(Sale);       

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        _mapper.Received(1).Map<Sale>(Arg.Is<CreateSaleCommand>(c =>
            c.CustomerId == command.CustomerId &&
            c.SaleItems == command.SaleItems));
    }

    public static ICollection<SaleItems> GetSaleItems(Guid saleId, List<string> codeProducts)
    {
        return new List<SaleItems>()
        {
            new SaleItems { SaleId= saleId, CodeProduct = codeProducts[0], Quantities = 1, UnitPrices = 10 },
            new SaleItems { SaleId= saleId, CodeProduct = codeProducts[1], Quantities = 2, UnitPrices = 20 }
        };
    }

    public static Guid GetCustomerId()
    {
        var customerRepository = Substitute.For<ICustomerRepository>();
        var customers = customerRepository.GetAllAsync().Result;
        return customers.Count() == 0 ? Guid.NewGuid() : customers.First().Id;
    }

    public static List<string> GetCodesProducts()
    {
        var productRepository = Substitute.For<IProductRepository>();
        var products = productRepository.GetAllAsync().Result;
        var retorno = new List<string>();
        foreach (var product in products)
        {
            retorno.Add(product.Code);
        }

        if (retorno.Count ==0) { retorno.Add("A00100"); retorno.Add("A00101"); }
        return retorno;
    }

}
