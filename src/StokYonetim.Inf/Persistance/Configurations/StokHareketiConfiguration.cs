using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StokYonetim.Domain.Entities;
using StokYonetim.Inf.Persistance.Configurations;

namespace StokYonetim.Infrastructure.Persistence.Configurations;

public class StokHareketiConfiguration : BaseEntityConfiguration<StokHareketi>
{
    public override void Configure(EntityTypeBuilder<StokHareketi> builder)
    {
        base.Configure(builder);

        builder.ToTable("StokHareketleri", t =>
            t.HasCheckConstraint("CK_StokHareketleri_Miktar", "[Miktar] > 0"));

        builder.Property(h => h.Tip).HasConversion<string>().HasMaxLength(20);
        builder.Property(h => h.Miktar).HasPrecision(18, 3);
        builder.Property(h => h.Aciklama).HasMaxLength(500);

        builder.Property(h => h.OlusturmaTarihi)
               .HasDefaultValueSql("SYSDATETIMEOFFSET()");

        builder.HasIndex(h => new { h.UrunId, h.DepoId, h.IslemTarihi });
        builder.HasIndex(h => h.TransferId).HasFilter("[TransferId] IS NOT NULL");

        builder.HasOne(h => h.Urun).WithMany().HasForeignKey(h => h.UrunId);
        builder.HasOne(h => h.Depo).WithMany().HasForeignKey(h => h.DepoId);
        builder.HasOne(h => h.Tedarikci).WithMany().HasForeignKey(h => h.TederikciId);
    }
}