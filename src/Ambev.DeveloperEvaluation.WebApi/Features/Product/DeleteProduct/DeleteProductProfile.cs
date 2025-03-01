using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;

/// <summary>
/// Profile for mapping DeleteProduct feature requests to commands
/// </summary>
public class DeleteProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for DeleteProduct feature
    /// </summary>
    public DeleteProductProfile()
    {
        CreateMap<string, Application.Products.DeleteProduct.DeleteProductCommand>()
            .ConstructUsing(Code => new Application.Products.DeleteProduct.DeleteProductCommand(Code));
    }
}
