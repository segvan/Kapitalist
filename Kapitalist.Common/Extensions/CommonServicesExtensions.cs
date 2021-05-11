using System;
using System.IO;
using System.Reflection;
using Kapitalist.Common.HttpHandlers;
using Kapitalist.Common.Rpc.Client;
using Kapitalist.Common.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

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

        public static RpcClientBuilder AddRpcClients(this IServiceCollection serviceCollection)
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("Rpc/RpcClientSettings.json");
            builder.AddEnvironmentVariables();
            var rpcConfiguration = builder.Build();

            var configurationSection = rpcConfiguration.GetSection("RpcClients");

            if (!configurationSection.Exists())
            {
                throw new Exception("Cannot find configuration RpcClients section");
            }

            return new RpcClientBuilder(serviceCollection, configurationSection);
        }

        public static IServiceCollection AddCommonServices(this IServiceCollection services)
        {
            services.AddSingleton<IDateTimeService, DateTimeService>();

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services, string title, string version)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(version, new OpenApiInfo {Title = title, Version = version});

                var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            return services;
        }
    }
}