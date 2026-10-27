using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StokYonetim.Inf.Persistance;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace StokYonetim.Inf
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInf(this IServiceCollection services, string connectionString)
        {

            services.AddDbContext<StokDbContext>(options => 
            options.UseSqlServer(connectionString));
          

            return services;
        }
    }
}
