using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{    
    public class SaleItemsConfiguration : IEntityTypeConfiguration<SaleItems>
    {
        public void Configure(EntityTypeBuilder<SaleItems> builder)
        {
            builder.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            builder.Property(e => e.Quantities).HasDefaultValue(1);
            builder.HasOne(d => d.Product).WithMany(p => p.SaleItems).HasConstraintName("FK_SaleItems_ProductId");
            builder.HasOne(d => d.Sale).WithMany(p => p.SaleItems).HasConstraintName("FK_SaleItems_SaleId");
        }
    }
}
