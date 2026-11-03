using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StokYonetim.Domain.Entities;
using StokYonetim.Inf.Persistance.Configurations;

namespace StokYonetim.Infrastructure.Persistence.Configurations;

public class AyarlarConfiguration : BaseEntityConfiguration<Ayarlar>
{
    public override void Configure(EntityTypeBuilder<Ayarlar> builder)
    {
        base.Configure(builder);

        builder.ToTable("Ayarlar", t =>
            t.HasCheckConstraint("CK_Ayarlar_TekSatir", "[Id] = 1"));

        builder.Property(a => a.Id).ValueGeneratedNever();

        builder.HasData(new { Id = 1 });
    }
}
