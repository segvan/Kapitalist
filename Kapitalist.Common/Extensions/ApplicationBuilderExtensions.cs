using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Kapitalist.Common.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseProdExceptionHandler(this IApplicationBuilder appBuilder)
        {
            appBuilder.UseExceptionHandler(a =>
            {
                a.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync("Internal Server Error.");
                });
            });
        }
    }
}