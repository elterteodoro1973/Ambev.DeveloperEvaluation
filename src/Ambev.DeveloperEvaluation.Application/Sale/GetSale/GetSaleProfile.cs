using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Profile for mapping between Sale entity and GetSaleResponse
/// </summary>
public class GetSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetSale operation
    /// </summary>
    public GetSaleProfile()
    {
        //CreateMap<Sale, GetSaleResult>();
        //CreateMap<GetSaleResult, Sale>();

        CreateMap<Sale, GetSaleResult>()
         .ForMember(c => c.SaleItems, m => m.MapFrom(c => c.SaleItems.Select(x => new GetSaleItemsResult
         {
             SaleId = x.SaleId,
             CodeProduct = x.CodeProduct,
             NameProduct = x.CodeProductNavigation.Description,
             Quantities = x.Quantities,
             UnitPrices = x.UnitPrices,
         }).ToList()));
    }
}
