using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

/// <summary>
/// Profile for mapping GetProduct feature requests to commands
/// </summary>
public class GetProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetProduct feature
    /// </summary>
    public GetProductProfile()
    {
        CreateMap<string, Application.Products.GetProduct.GetProductCommand>().ConstructUsing(Description => new Application.Products.GetProduct.GetProductCommand(Description));
        CreateMap<GetProductResult, GetProductResponse>();
    }
}
