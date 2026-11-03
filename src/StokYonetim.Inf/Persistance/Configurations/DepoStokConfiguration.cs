using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StokYonetim.Domain.Entities;
using StokYonetim.Inf.Persistance.Configurations;

namespace StokYonetim.Infrastructure.Persistence.Configurations;

public class DepoStokConfiguration : BaseEntityConfiguration<DepoStok>
{
    public override void Configure(EntityTypeBuilder<DepoStok> builder)
    {
        base.Configure(builder);

        builder.ToTable("DepoStoklar", t =>
        {
            t.HasCheckConstraint("CK_DepoStoklar_Miktar", "[Miktar] >= 0");
            t.HasCheckConstraint("CK_DepoStoklar_KritikSeviye", "[KritikSeviye] >= 0");
        });

        builder.Property(d => d.Miktar).HasPrecision(18, 3);
        builder.Property(d => d.KritikSeviye).HasPrecision(18, 3);

        builder.HasIndex(d => new { d.DepoId, d.UrunId }).IsUnique();

        builder.HasIndex(d => new { d.DepoId, d.Miktar })
               .IncludeProperties(d => d.KritikSeviye);

        builder.HasOne(d => d.Depo).WithMany().HasForeignKey(d => d.DepoId);
        builder.HasOne(d => d.Urun).WithMany().HasForeignKey(d => d.UrunId);
    }
}