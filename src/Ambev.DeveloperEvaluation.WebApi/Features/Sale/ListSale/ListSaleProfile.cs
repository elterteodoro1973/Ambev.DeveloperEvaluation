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
        CreateMap<Sale, ListSaleItemsResponse>();
        CreateMap<SaleItems, Sale>();
        CreateMap<Sale, ListSaleResponse>();
        CreateMap<ListSaleResponse, Sale>();
    }
}
