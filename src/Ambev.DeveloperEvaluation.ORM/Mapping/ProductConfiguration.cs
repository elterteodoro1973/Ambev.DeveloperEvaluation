using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{   

    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            builder.Property(e => e.Description).IsFixedLength();
            builder.Property(e => e.Price).HasDefaultValueSql("0");
            builder.Property(e => e.QuantityInStock).HasDefaultValue(0);
        }
    }
}
