using MediaService.Extensions.DI;
using MediaService.Extensions.Middlewares;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NextCloud.Lib.Extensions;
using ServiceShared.Lib.Swagger;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace MediaService
{
    public class Startup
    {
        public IConfiguration _configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddFluentValidation()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services
                .AddSwaggerConfiguration()
                .AddBindings()
                .AddNextCloud(_configuration);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            
            app.UseMiddlewares();
            app.UseMvc();
            app.UseSwagger();
            app.UseCookiePolicy();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}