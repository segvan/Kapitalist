using AutoMapper;
using Kapitalist.Common;
using Kapitalist.RatesGrpcClient.Proto;
using Kapitalist.Worker.Adapters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Kapitalist.Worker
{
    public static class WorkerServicesExtensions
    {
        public static IServiceCollection AddWorker(this IServiceCollection services, HostBuilderContext context)
        {
            services.AddHostedService<RatesWorkerService>();
            services.Configure<WorkerSettings>(context.Configuration.GetSection("PlatformSettings"));

            services.AddGenericHttpClient();
            services.AddCommonServices();
            services.AddRpcClients()
                .AddClient<RatesService.RatesServiceClient>();
            
            services.AddAutoMapper(expression => expression.AddProfile<AutoMapperProfile>());

            services.AddSingleton<IRatesAdapter, ShitcoinsRatesAdapter>();

            return services;
        }
    }
}