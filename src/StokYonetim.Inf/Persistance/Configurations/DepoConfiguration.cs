using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StokYonetim.Domain.Entities;
using StokYonetim.Inf.Persistance.Configurations;

namespace StokYonetim.Infrastructure.Persistence.Configurations;

public class DepoConfiguration : BaseEntityConfiguration<Depo>
{
    public override void Configure(EntityTypeBuilder<Depo> builder)
    {
        base.Configure(builder);

        builder.ToTable("Depolar");
        builder.Property(d => d.Ad).HasMaxLength(100);
        builder.HasIndex(d => d.Ad).IsUnique();
    }
}