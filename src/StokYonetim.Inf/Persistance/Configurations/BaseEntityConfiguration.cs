using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StokYonetim.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StokYonetim.Inf.Persistance.Configurations
{
    public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder) {

            builder.HasKey(e => e.Id);
            builder.Property(e => e.RowVersion).IsRowVersion();
        }
    }
}
