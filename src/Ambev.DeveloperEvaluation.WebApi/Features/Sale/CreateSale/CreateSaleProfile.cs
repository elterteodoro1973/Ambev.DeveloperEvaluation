using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Profile for mapping between Application and API CreateSale responses
/// </summary>
public class CreateSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateSale feature
    /// </summary>
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleRequest, CreateSaleCommand>()
                 .ForMember(c => c.CustomerId, m => m.MapFrom(c => c.CustomerId))
                 .ForMember(c => c.SaleItems, m => m.MapFrom(c => c.SaleItems.Select(c => new CreateSaleItemsCommand
                 {
                     CodeProduct = c.CodeProduct,
                     Quantities = c.Quantities,
                     UnitPrices = c.UnitPrices,
                 }).ToList())); ;

        CreateMap<CreateSaleResult, CreateSaleResponse>()
                .ForMember(c => c.SaleItems, m => m.MapFrom(c => c.SaleItems.Select(c => new CreateSaleItemsResponse
                {
                    CodeProduct = c.CodeProduct,
                    Quantities = c.Quantities,
                    UnitPrices = c.UnitPrices,
                }).ToList()));

       


    }
}
