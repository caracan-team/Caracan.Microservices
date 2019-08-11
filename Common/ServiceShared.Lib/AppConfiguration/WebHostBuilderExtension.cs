using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace ServiceShared.Lib.AppConfiguration
{
    public static class WebHostBuilderExtension
    {
        public static IWebHostBuilder ConfigureApp(this IWebHostBuilder webHostBuilder)
        {
            return webHostBuilder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                var env = hostingContext.HostingEnvironment;

                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);


                config.AddEnvironmentVariables();
            }); 
        }
    }
}