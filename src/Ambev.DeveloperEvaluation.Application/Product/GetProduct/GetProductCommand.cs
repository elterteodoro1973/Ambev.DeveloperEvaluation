using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

/// <summary>
/// Command for retrieving a Product by their ID
/// </summary>
public record GetProductCommand : IRequest<GetProductResult>
{   

    public string Code { get; }

    /// <summary>
    /// Initializes a new instance of GetProductCommand
    /// </summary>
    /// <param name="id">The ID of the Product to retrieve</param>
    public GetProductCommand(string code)
    {
        Code = code;
    }
    
}
