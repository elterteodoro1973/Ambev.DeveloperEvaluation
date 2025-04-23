using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Profile for mapping between Sale entity and CreateSaleResponse
/// </summary>
public class CreateSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateSale operation
    /// </summary>
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleCommand, Sale>()
                .ForMember(c => c.Id, m => m.MapFrom(c => Guid.NewGuid()))
                .ForMember(c => c.CustomerId, m => m.MapFrom(c => c.CustomerId))
                .ForMember(c => c.SaleItems, m => m.MapFrom(c => c.SaleItems.Select(c => new SaleItems
                {                    
                    CodeProduct = c.CodeProduct,
                    Quantities = c.Quantities,
                    UnitPrices = c.UnitPrices,
                }).ToList())) ;

        CreateMap<CreateSaleCommand, CreateSaleResult>()
             .ForMember(c => c.SaleItems, m => m.MapFrom(c => c.SaleItems.Select(c => new CreateSaleItemsResult
             {
                 CodeProduct = c.CodeProduct,
                 Quantities = c.Quantities,
                 UnitPrices = c.UnitPrices,
             }).ToList()));


        CreateMap<Sale, CreateSaleResult>()
             .ForMember(c => c.SaleItems, m => m.MapFrom(c => c.SaleItems.Select(c => new CreateSaleItemsResult
             {
                 CodeProduct = c.CodeProduct,
                 Quantities = c.Quantities,
                 UnitPrices = c.UnitPrices,
             }).ToList()));
    }


}
