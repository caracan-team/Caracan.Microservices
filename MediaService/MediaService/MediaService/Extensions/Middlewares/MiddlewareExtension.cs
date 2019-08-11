using MediaService.Middleware;
using Microsoft.AspNetCore.Builder;

namespace MediaService.Extensions.Middlewares
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseMiddlewares(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<GlobalExceptionHandlerMiddleware>();

            return builder;
        }
    }
}
