using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{    
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            builder.HasOne(d => d.Customer).WithMany(p => p.Sale)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sale_Customer_Id");
        }
    }

}
