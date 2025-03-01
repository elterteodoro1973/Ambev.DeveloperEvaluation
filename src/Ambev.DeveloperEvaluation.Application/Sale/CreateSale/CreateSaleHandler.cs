using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Common.Security;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Handler for processing CreateSaleCommand requests
/// </summary>
public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _SaleRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;

    /// <summary>
    /// Initializes a new instance of CreateSaleHandler
    /// </summary>
    /// <param name="SaleRepository">The Sale repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for CreateSaleCommand</param>
    public CreateSaleHandler(ISaleRepository SaleRepository, IMapper mapper, IPasswordHasher passwordHasher)
    {
        _SaleRepository = SaleRepository;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    /// <summary>
    /// Handles the CreateSaleCommand request
    /// </summary>
    /// <param name="command">The CreateSale command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created Sale details</returns>
    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        //var Sale = _mapper.Map<Sale>(command);

        var id = Guid.NewGuid();
        var sale = new Sale
        {
            Id = id,
            CustomerId = command.CustomerId,
            SaleItems = command.SaleItems.Select(c => new SaleItems
            {                
                SaleId = id,
                CodeProduct = c.CodeProduct,
                Quantities = c.Quantities,
                UnitPrices = c.UnitPrices
            }).ToList()
        };

        var valueTotal = command.SaleItems.Sum(c => c.Quantities * c.UnitPrices);
        var grupoSaleItems = command.SaleItems.GroupBy(c => c.CodeProduct)
                                    .Select(g => new
                                    {
                                        ProductId = g.Key,
                                        Total = g.Sum(c => c.Quantities)
                                    }).OrderBy(c=>c.Total);

        var discounts = 0;

        if (grupoSaleItems.Where(c => c.Total > 20).Any())
        {
            throw new InvalidOperationException("SaleItems with more than 20 items");
        }
        else if (grupoSaleItems.Where(c => c.Total > 4 && c.Total < 10 ).Any())
        {
            discounts = 10;
        }
        else if (grupoSaleItems.Where(c => c.Total > 10 && c.Total < 10).Any())
        {
            discounts = 20;
        }

        sale.SaleDate = DateTime.Now;
        sale.TotalGrossValue = valueTotal;
        sale.TotalNetValue = valueTotal -(valueTotal *(discounts/100));
        sale.Discounts = discounts;

        
        var createdSale = await _SaleRepository.CreateAsync(sale, cancellationToken);
        var result = _mapper.Map<CreateSaleResult>(createdSale);
        return result;
    }
}
