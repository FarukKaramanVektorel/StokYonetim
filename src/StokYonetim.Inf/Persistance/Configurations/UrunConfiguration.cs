using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StokYonetim.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StokYonetim.Inf.Persistance.Configurations
{
    public class UrunConfiguration:BaseEntityConfiguration<Urun>
    {
        public override void Configure(EntityTypeBuilder<Urun> builder)
        {
            base.Configure(builder);

            builder.ToTable("Urunler");
            builder.Property(u => u.Ad).HasMaxLength(150);
            builder.HasIndex(u => u.Ad);

            builder.Property(u => u.Barkod).HasMaxLength(50).IsUnicode(false);
            builder.HasIndex(u => u.Barkod)
                .IsUnique()
                .HasFilter("[Barkod] IS NOT NULL");

            builder.Property(u => u.Birim).HasConversion<string>().HasMaxLength(10);
            builder.Property(u => u.VarsayilanKritikSeviye).HasPrecision(18, 3);

            builder.HasOne(u => u.Kategori)
                .WithMany()
                .HasForeignKey(u => u.KategoriId);


            builder.HasOne(u => u.Tedarikci)
                .WithMany()
                .HasForeignKey(u => u.TedarikciId);


        }
    }
}
