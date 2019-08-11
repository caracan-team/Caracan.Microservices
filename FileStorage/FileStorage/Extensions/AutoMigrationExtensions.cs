using FileStorage.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileStorage.Extensions
{
    public static class AutoMigrationExtensions
    {
        public static IServiceCollection AddAutoMigration<T>(this IServiceCollection services) where T : class, IAutoMigration
        {
            services.AddScoped<IAutoMigration, T>();
            return services;
        }

        public static IApplicationBuilder UseAutoMigration(this IApplicationBuilder app)
        {
            using (var scoped = app.ApplicationServices.CreateScope())
            {
                var autoMigration = scoped.ServiceProvider.GetService<IAutoMigration>();
                autoMigration.Migrate();
            }
            return app;
        }
    }
}
