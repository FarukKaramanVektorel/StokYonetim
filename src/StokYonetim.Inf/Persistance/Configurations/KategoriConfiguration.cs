using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StokYonetim.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StokYonetim.Inf.Persistance.Configurations
{
    public class KategoriConfiguration:BaseEntityConfiguration<Kategori>
    {
        public override void Configure(EntityTypeBuilder<Kategori> builder)
        {
            base.Configure(builder);

            builder.ToTable("Kategoriler", t =>
            t.HasCheckConstraint("CK_Kategoriler_Seviye", "[Seviye] BETWEEN 1 AND 5"));
            builder.Property(k => k.Id).UseHiLo("KategoriIdSirasi");
            builder.Property(k => k.Ad).HasMaxLength(100);
            builder.Property(k => k.Yol).HasMaxLength(100).IsUnicode(false);
            builder.HasIndex(k => k.Yol).IsUnique();
            builder.HasOne(K => K.UstKategori)
                .WithMany(k=>k.AltKategoriler)
                .HasForeignKey(k=>k.UstKategoriId);

            builder.HasIndex(k => new { k.UstKategoriId, k.Ad })
                .IsUnique()
                .HasFilter(null);
        }


    }
}
