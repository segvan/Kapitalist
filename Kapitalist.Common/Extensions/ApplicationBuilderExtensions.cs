using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Kapitalist.Common.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseProdExceptionHandler(this IApplicationBuilder appBuilder)
        {
            appBuilder.UseExceptionHandler(a =>
            {
                a.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync("Internal Server Error.");
                });
            });

            return appBuilder;
        }

        public static IApplicationBuilder UseSwagger(this IApplicationBuilder appBuilder, string title, string version)
        {
            appBuilder.UseSwagger();
            appBuilder.UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{title} - {version}");
            });

            return appBuilder;
        }
    }
}