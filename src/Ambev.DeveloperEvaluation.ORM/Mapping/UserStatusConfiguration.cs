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
    public class UserStatusConfiguration : IEntityTypeConfiguration<UserStatus>
    {
        public void Configure(EntityTypeBuilder<UserStatus> builder)
        {
            builder.ToTable("UserStatus", "DeveloperEvaluation");
            builder.HasKey(e => e.Id).HasName("UserStatus_pkey");
            builder.Property(e => e.Id).ValueGeneratedNever().HasDefaultValue(0);
            builder.Property(e => e.Description).IsFixedLength();
        }
    }

}
