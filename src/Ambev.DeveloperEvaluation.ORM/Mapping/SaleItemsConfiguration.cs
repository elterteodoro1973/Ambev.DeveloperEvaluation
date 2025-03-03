using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{    
    public class SaleItemsConfiguration : IEntityTypeConfiguration<SaleItems>
    {
        public void Configure(EntityTypeBuilder<SaleItems> builder)
        {
            builder.Property(e => e.Quantities).HasDefaultValue(1);
            builder.HasOne(d => d.CodeProductNavigation).WithMany(p => p.SaleItems)
                .HasPrincipalKey(p => p.Code)
                .HasForeignKey(d => d.CodeProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SaleItems_ProductCode");
            builder.HasOne(d => d.Sale).WithMany(p => p.SaleItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SaleItems_SaleId");
        }
    }
}
