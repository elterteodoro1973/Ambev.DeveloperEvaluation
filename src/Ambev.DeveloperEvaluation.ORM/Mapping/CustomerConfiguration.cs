using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.RegularExpressions;


namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {            
            builder.ToTable("Customer", "DeveloperEvaluation", tb => tb.HasComment("Cliente"));
            builder.HasKey(e => e.Id).HasName("Customer_pkey");            
            builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");            
            builder.Property(e => e.Name).IsFixedLength();
            builder.Property(e => e.email).IsFixedLength();
            builder.Property(e => e.phone).IsFixedLength();
        }
    }
}
