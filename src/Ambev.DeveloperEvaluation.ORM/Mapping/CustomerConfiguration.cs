using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(e => e.Id).HasName("Customer_pkey");
            builder.ToTable("Customer", "DeveloperEvaluation", tb => tb.HasComment("Cliente"));
            builder.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            builder.Property(e => e.Name).IsFixedLength();
            builder.Property(e => e.Email).IsFixedLength();
            builder.Property(e => e.Phone).IsFixedLength();
        }
    }
}
