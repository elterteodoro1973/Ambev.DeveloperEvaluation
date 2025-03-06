using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;


namespace Ambev.DeveloperEvaluation.ORM.Services;

/// <summary>
/// Handler for processing GetUserCommand requests
/// </summary>
public class SaleService : ISaleService
{
    private readonly ISaleRepository _saleRepository;
    private readonly IProductRepository _productRepository;

    public SaleService(ISaleRepository saleRepository, IProductRepository productRepository)
    {
        _saleRepository = saleRepository;
        _productRepository = productRepository;
    }
    /// <summary>
    /// Handles the GetUserCommand request
    /// </summary>   
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale details if found</returns>
    public async Task<IEnumerable<Sale>> GetAllAsync(CancellationToken cancellationToken)
    {
        var sales = await _saleRepository.GetAllAsync(cancellationToken);
        if (sales == null)
            throw new KeyNotFoundException($"Sale not found"); 

        return sales;
    }

    

}
