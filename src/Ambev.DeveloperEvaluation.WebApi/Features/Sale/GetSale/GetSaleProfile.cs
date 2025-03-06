using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sale.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// Profile for mapping GetSale feature requests to commands
/// </summary>
public class GetSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetSale feature
    /// </summary>
    public GetSaleProfile()
    {
        CreateMap<Guid, Application.Sales.GetSale.GetSaleCommand>().ConstructUsing(id => new Application.Sales.GetSale.GetSaleCommand(id));        
        CreateMap<GetSaleResult, GetSaleResponse>()
            .ForMember(c => c.CustomerName, m => m.MapFrom(c => c.Customer.Name))
            .ForMember(c => c.SaleItems, m => m.MapFrom(c => c.SaleItems.Select(x => new GetSaleItemsResponse
            {
                SaleId = x.SaleId,
                CodeProduct = x.CodeProduct,
                NameProduct = x.NameProduct,
                Quantities = x.Quantities,
                UnitPrices = x.UnitPrices,
            }).ToList()));
    }
}
