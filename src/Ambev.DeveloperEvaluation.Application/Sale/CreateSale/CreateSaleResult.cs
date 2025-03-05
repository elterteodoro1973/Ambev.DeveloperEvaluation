using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Represents the response returned after successfully creating a new Sale.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the newly created Sale,
/// which can be used for subsequent operations or reference.
/// </remarks>
public class CreateSaleResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the newly created Sale.
    /// </summary>
    /// <value>A GUID that uniquely identifies the created Sale in the system.</value>
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public DateTime? SaleDate { get; set; }
    
    public decimal? TotalGrossValue { get; set; }
    
    public decimal? Discounts { get; set; }
    
    public decimal? TotalNetValue { get; set; }

    public bool? Cancelled { get; set; } = false;
    
    public virtual Customer Customer { get; set; }
    
    public virtual ICollection<SaleItems> SaleItems { get; set; } = new List<SaleItems>();


}
