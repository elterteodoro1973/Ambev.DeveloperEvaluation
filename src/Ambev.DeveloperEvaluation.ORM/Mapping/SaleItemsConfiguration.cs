using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{    
    public class SaleItemsConfiguration : IEntityTypeConfiguration<SaleItems>
    {
        public void Configure(EntityTypeBuilder<SaleItems> builder)
        {            
            builder.ToTable("SaleItems", "DeveloperEvaluation");            
            builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
            builder.Property(e => e.Quantities).HasDefaultValue(1);
            builder.HasOne(d => d.Product).WithMany(p => p.SaleItems).HasConstraintName("FK_SaleItems_ProductId");
            builder.HasOne(d => d.Sales).WithMany(p => p.SaleItems).HasConstraintName("FK_SaleItems_SaleId");
        }
    }
}
