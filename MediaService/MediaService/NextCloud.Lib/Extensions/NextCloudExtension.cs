using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NextCloud.Lib.Client;
using NextCloud.Lib.Configuration;

namespace NextCloud.Lib.Extensions
{
    public static class NextCloudExtension
    {
        public static IServiceCollection AddNextCloud(this IServiceCollection services, IConfiguration configuration)
        {
            var nextCloudConfig = new NextCloudConfig();
            configuration.GetSection("NextCloudConfig").Bind(nextCloudConfig);
            
            services.AddSingleton(nextCloudConfig);
            services.AddScoped<INextCloudClient, NextCloudClient>();
            
            return services;
        }
    }
}