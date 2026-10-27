using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace StokYonetim.Inf.Persistance
{
    public class StokDbContext(DbContextOptions<StokDbContext> option):DbContext(option)
    {
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
