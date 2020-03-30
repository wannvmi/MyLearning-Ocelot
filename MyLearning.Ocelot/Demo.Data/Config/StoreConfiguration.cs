using System;
using System.Collections.Generic;
using System.Text;
using Demo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.Data.Config
{
    public class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.ToTable("store");

            builder.HasKey(x => x.Id);

            builder.HasQueryFilter(x => x.IsDelete == 0);
        }
    }
}
