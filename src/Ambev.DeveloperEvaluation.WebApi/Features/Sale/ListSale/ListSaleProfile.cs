using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Users.GetUser;

/// <summary>
/// Profile for mapping between User entity and GetUserResponse
/// </summary>
public class ListSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetUser operation
    /// </summary>
    public ListSaleProfile()
    {        
        CreateMap<Sale, ListSaleResponse>()         
         .ForMember(c => c.SaleItems, m => m.MapFrom(c => c.SaleItems.Select(x => new ListSaleItemsResponse   {
                                                          SaleId = x.SaleId,
                                                          CodeProduct = x.CodeProduct,
                                                          NameProduct = x.CodeProductNavigation.Description,
                                                          Quantities = x.Quantities,
                                                          UnitPrices = x.UnitPrices,
                                                    }).ToList()));        
    }
}
