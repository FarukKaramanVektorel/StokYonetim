using Microsoft.EntityFrameworkCore;
using StokYonetim.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StokYonetim.Inf.Persistance
{
    public class StokDbContext(DbContextOptions<StokDbContext> option):DbContext(option)
    {
        public DbSet<Kategori> Kategoriler => Set<Kategori>();
        public DbSet<Tedarikci> Tedarikciler => Set<Tedarikci>();
        public DbSet<Depo> Depolar => Set<Depo>();
        public DbSet<Urun> Urunler => Set<Urun>();
        public DbSet<DepoStok> DepoStoklar => Set<DepoStok>();
        public DbSet<StokHareketi> StokHareketleri => Set<StokHareketi>();
        public DbSet<Ayarlar> Ayarlar => Set<Ayarlar>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Turkish_100_CI_AS");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StokDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
            configurationBuilder.Properties<string>().HaveMaxLength(200);
        }
    }
}
