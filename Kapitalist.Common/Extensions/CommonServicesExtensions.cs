using System;
using Kapitalist.Common.HttpHandlers;
using Kapitalist.Common.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Kapitalist.Common
{
    public static class CommonServicesExtensions
    {
        public static IServiceCollection AddGenericHttpClient(this IServiceCollection services)
        {
            services.AddHttpClient("GenericClient", client =>
                {
                    client.Timeout = new TimeSpan(0, 0, 20);
                    client.DefaultRequestHeaders.Clear();
                })
                .AddHttpMessageHandler(handler =>
                    new LoggerDelegatingHandler(handler.GetService<ILogger<LoggerDelegatingHandler>>()))
                .AddHttpMessageHandler(handler => new RetryPolicyDelegatingHandler(3))
                .AddHttpMessageHandler(handler => new EnsureStatusCodeDelegatingHandler());

            return services;
        }

        public static IServiceCollection AddCommonServices(this IServiceCollection services)
        {
            services.AddSingleton<IDateTimeService, DateTimeService>();

            return services;
        }
    }
}