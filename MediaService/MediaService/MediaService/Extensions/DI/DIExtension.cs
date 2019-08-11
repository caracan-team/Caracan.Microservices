using MediaService.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MediaService.Extensions.DI
{
    public static class DIExtension
    {
        /// <summary>
        /// Register bindings 
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddBindings(this IServiceCollection services)
        {
            services.AddScoped<IMediaService, Application.Services.MediaService>();
            return services;
        }
    }
}