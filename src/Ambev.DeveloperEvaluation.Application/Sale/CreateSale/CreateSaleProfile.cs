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
                .ForMember(c => c.CustomerId, m => m.MapFrom(c => c.CustomerId));
        CreateMap<Sale, CreateSaleResult>();
    }

    
}
