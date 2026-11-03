using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StokYonetim.Domain.Entities;
using StokYonetim.Inf.Persistance.Configurations;

namespace StokYonetim.Infrastructure.Persistence.Configurations;

public class TedarikciConfiguration : BaseEntityConfiguration<Tedarikci>
{
    public override void Configure(EntityTypeBuilder<Tedarikci> builder)
    {
        base.Configure(builder);

        builder.ToTable("Tedarikciler");
        builder.Property(t => t.Ad).HasMaxLength(150);
        builder.HasIndex(t => t.Ad).IsUnique();
        builder.Property(t => t.Telefon).HasMaxLength(20).IsUnicode(false);
        builder.Property(t => t.Eposta).HasMaxLength(150).IsUnicode(false);
    }
}