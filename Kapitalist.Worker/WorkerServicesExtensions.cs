using Kapitalist.Common;
using Kapitalist.Worker.Adapters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Kapitalist.Worker
{
    public static class WorkerServicesExtensions
    {
        public static IServiceCollection AddWorker(this IServiceCollection services, HostBuilderContext context)
        {
            services.AddHostedService<Worker>();
            services.Configure<WorkerSettings>(context.Configuration.GetSection("PlatformSettings"));

            services.AddGenericHttpClient();
            services.AddCommonServices();

            services.AddSingleton<IRatesAdapter, ShitcoinsRatesAdapter>();

            return services;
        }
    }
}