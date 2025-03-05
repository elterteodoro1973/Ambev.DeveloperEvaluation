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
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;    

    /// <summary>
    /// Initializes a new instance of CreateSaleHandler
    /// </summary>
    /// <param name="SaleRepository">The Sale repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for CreateSaleCommand</param>
    public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
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
        var grupoSaleItems = command.SaleItems.GroupBy(c => c.CodeProduct)
                                    .Select(g => new
                                    {
                                        CodeProduct = g.Key,
                                        UnitPrices = g.First().UnitPrices,
                                        Quantities = g.Sum(c => c.Quantities)                                   
                                    }).OrderByDescending(c => c.UnitPrices);

        var id = Guid.NewGuid();
        var sale = new Sale
        {
            Id = id,
            CustomerId = command.CustomerId,
            SaleItems = grupoSaleItems.Select(c => new SaleItems
            {                
                SaleId = id,
                CodeProduct = c.CodeProduct,
                Quantities = c.Quantities,
                UnitPrices = c.UnitPrices,
            }).ToList()
        };

        var valueTotal = command.SaleItems.Sum(c => c.Quantities * c.UnitPrices);
        
        Decimal  discounts = 0;

        if (grupoSaleItems.Where(c => c.Quantities > 20).Any())
        {
            throw new InvalidOperationException("SaleItems with more than 20 items");
        }        
        else if (grupoSaleItems.Where(c => c.Quantities > 10 && c.Quantities <= 20).Any())
        {
            discounts = 20;
        }
        else if (grupoSaleItems.Where(c => c.Quantities > 4 && c.Quantities <= 9).Any())
        {
            discounts = 10;
        }        

        sale.SaleDate = DateTime.UtcNow; 
        sale.TotalGrossValue = valueTotal; 
        sale.TotalNetValue = valueTotal - (valueTotal * (discounts / 100));
        sale.Discounts = discounts;
        sale.Cancelled = false;

        var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);
        var result = _mapper.Map<CreateSaleResult>(createdSale);
        return result;
    }
}
