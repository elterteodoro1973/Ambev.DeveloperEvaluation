using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of ISaleRepository using Entity Framework Core
/// </summary>
public class SaleRepository : ISaleRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of SaleRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public SaleRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves all Sales from the database
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of all Sales</returns>
    public async Task<IEnumerable<Sale>> GetAllAsync(CancellationToken cancellationToken = default)
    {  
        return await _context.Sale
            .Include(c => c.SaleItems)
            .ThenInclude(si => si.CodeProductNavigation)
            .Include(c => c.Customer)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Creates a new Sale in the database
    /// </summary>
    /// <param name="Sale">The Sale to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created Sale</returns>
    public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        var id = Guid.NewGuid();

        sale.SaleItems = await AgrupamentoItens(sale.SaleItems.Select(c => new SaleItems
        {
            //SaleId = id,
            CodeProduct = c.CodeProduct,
            Quantities = c.Quantities,
            UnitPrices = c.UnitPrices,
        }).ToList(), cancellationToken);

        

        sale.SaleDate = DateTime.UtcNow;
        sale.TotalGrossValue = sale.SaleItems.Sum(c => c.Quantities * c.UnitPrices);
        sale.Discounts = await Desconto(sale.SaleItems, cancellationToken);
        sale.TotalNetValue = sale.TotalGrossValue - (sale.TotalGrossValue * (sale.Discounts / 100));
        sale.Cancelled = false;

        await _context.Sale.AddAsync(sale, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return sale;
    }

   

    /// <summary>
    /// Retrieves a Sale by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the Sale</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Sale if found, null otherwise</returns>
    public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Sale
            .Include(c => c.SaleItems)
            .ThenInclude(si => si.CodeProductNavigation)
            .Include(c => c.Customer)
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }


    /// <summary>
    /// Deletes a Sale from the database
    /// </summary>
    /// <param name="id">The unique identifier of the Sale to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the Sale was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var Sale = await GetByIdAsync(id, cancellationToken);
        if (Sale == null)
            return false;

        await _context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            _context.SaleItems.RemoveRange(Sale.SaleItems);
            await _context.SaveChangesAsync(cancellationToken);

            _context.Sale.Remove(Sale);
            await _context.SaveChangesAsync(cancellationToken);

            await _context.Database.CommitTransactionAsync();
        }
        catch (Exception)
        {
            await _context.Database.RollbackTransactionAsync();
            throw;
        }


        return true;
    }


    public async Task<ICollection<SaleItems>> AgrupamentoItens(ICollection<SaleItems> saleItems, CancellationToken cancellationToken = default)
    {
        var agrupoSaleItems = saleItems.GroupBy(c => c.CodeProduct)
                                      .Select(g => new SaleItems
                                      {                                          
                                          CodeProduct = g.Key,
                                          //SaleId = g.First().SaleId,
                                          UnitPrices = g.First().UnitPrices,
                                          Quantities = g.Sum(c => c.Quantities)
                                      })
                                      .OrderByDescending(c => c.UnitPrices).ToList();

        return agrupoSaleItems;
    }

    public async Task<decimal> Desconto(ICollection<SaleItems> saleItems, CancellationToken cancellationToken = default)
    {
        var saleItemsAgrupados = await AgrupamentoItens(saleItems, cancellationToken);

        if (saleItemsAgrupados.Max(c => c.Quantities) < 4)        
            return 0; 

        return (saleItemsAgrupados.Any(c =>  c.Quantities >= 10)) ? 20 : 10;        
    }


    public async Task<bool> QuantidadeInvalida(ICollection<SaleItems> saleItems, CancellationToken cancellationToken = default)
    {
        var saleItemsAgrupados = await AgrupamentoItens(saleItems, cancellationToken);

        if (saleItemsAgrupados.Max(c => c.Quantities) > 20)
            return true;

        return false;
    }

   
}
