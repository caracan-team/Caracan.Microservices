using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace ServiceShared.Lib.Swagger
{
    /// <summary>
    /// Swagger config extension
    /// </summary>
    public static class SwaggerExtension
    {
        /// <summary>
        /// Add swagger configuration
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                var serviceName = Assembly.GetEntryAssembly().GetName().Name;
                
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = serviceName
                });

                var xmlFile = $"{serviceName}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            return services;
        }
    }
}