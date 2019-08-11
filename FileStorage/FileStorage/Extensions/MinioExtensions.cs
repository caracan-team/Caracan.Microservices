using FileStorage.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileStorage.Extensions
{
    public static class MinioExtensions
    {
        public static IServiceCollection AddMinioClient(this IServiceCollection services, IConfiguration configuration)
        {
            var minioConfiguration = configuration.GetSection("MinioConfiguration").Get<MinioConfiguration>();
            var client = new MinioClient(minioConfiguration.Endpoint, minioConfiguration.AccessKey, minioConfiguration.SecretKey);
            services.AddSingleton(client);

            return services;
        }
    }
}
