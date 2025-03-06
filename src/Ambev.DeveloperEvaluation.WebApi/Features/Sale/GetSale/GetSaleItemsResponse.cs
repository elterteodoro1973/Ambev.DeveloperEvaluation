namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.GetSale
{
    public class GetSaleItemsResponse
    {
        /// <summary>
        /// The unique SaleId of the Sale
        /// </summary>
        public Guid SaleId { get; set; }

        /// <summary>
        /// The unique Quantities of the Sale
        /// </summary>
        public int Quantities { get; set; }

        /// <summary>
        /// The unique UnitPrices of the Sale
        /// </summary>
        public decimal UnitPrices { get; set; }

        /// <summary>
        /// The unique CodeProduct of the Sale
        /// </summary>
        public string CodeProduct { get; set; }

        /// <summary>
        /// The unique CodeProduct of the Sale
        /// </summary>
        public string NameProduct { get; set; }
    }
}
