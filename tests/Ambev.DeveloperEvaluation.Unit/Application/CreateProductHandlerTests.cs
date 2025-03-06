using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain;
using AutoMapper;
using FluentAssertions;
using FluentAssertions.Equivalency;
using NSubstitute;
using Xunit;
using static System.Net.Mime.MediaTypeNames;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contains unit tests for the <see cref="CreateProductHandler"/> class.
/// </summary>
public class CreateProductHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;    
    private readonly CreateProductHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateProductHandlerTests"/> class.
    /// Sets up the test dependencies and creates fake data generators.
    /// </summary>
    public CreateProductHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateProductHandler(_productRepository, _mapper);
    }

    /// <summary>
    /// Tests that a valid Product creation request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid Product data When creating Product Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = CreateProductHandlerTestData.GenerateValidCommand();
        var Product = new Product
        {
            Id = Guid.NewGuid(),            
            Code = command.Code,            
            Description = command.Description,
            Image = command.Image,            
            Price = command.Price,
            QuantityInStock = command.QuantityInStock
        };

        var result = new CreateProductResult
        {
            Id = Product.Id,
        };

        _mapper.Map<Product>(command).Returns(Product);
        _mapper.Map<CreateProductResult>(Product).Returns(result);

        _productRepository.CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>()).Returns(Product);


        // When
        var createProductResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createProductResult.Should().NotBeNull();
        createProductResult.Id.Should().Be(Product.Id);
        await _productRepository.Received(1).CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid Product creation request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid Product data When creating Product Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        var command = new CreateProductCommand();
        var act = () => _handler.Handle(command, CancellationToken.None);
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    /// <summary>
    /// Tests that the mapper is called with the correct command.
    /// </summary>
    [Fact(DisplayName = "Given valid command When handling Then maps command to Product entity")]
    public async Task Handle_ValidRequest_MapsCommandToProduct()
    {
        // Given
        var command = CreateProductHandlerTestData.GenerateValidCommand();
        var Product = new Product
        {
            Id = Guid.NewGuid(),
            Code = command.Code,            
            Description = command.Description,
            Image = command.Image,            
            Price = command.Price,
            QuantityInStock = command.QuantityInStock
        };

        _mapper.Map<Product>(command).Returns(Product);
        _productRepository.CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>()).Returns(Product);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        _mapper.Received(1).Map<Product>(Arg.Is<CreateProductCommand>(c =>            
            c.Code == command.Code &&           
            c.Description == command.Description &&
            c.Image == command.Image &&            
            c.Price == command.Price &&
            c.QuantityInStock == command.QuantityInStock));
    }
}
