using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Kapitalist.RatesGrpcClient.Proto;
using Kapitalist.Worker.Adapters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Kapitalist.Worker
{
    public class RatesWorkerService : BackgroundService
    {
        private readonly WorkerSettings workerSettings;
        private readonly ILogger<RatesWorkerService> logger;
        private readonly IMapper mapper;
        private readonly RatesService.RatesServiceClient ratesClient;
        private readonly IRatesAdapter[] adapters;

        public RatesWorkerService(IOptions<WorkerSettings> workerSettings, ILogger<RatesWorkerService> logger, IMapper mapper,
            IEnumerable<IRatesAdapter> adapters, RatesService.RatesServiceClient ratesClient)
        {
            this.workerSettings = workerSettings.Value;
            this.logger = logger;
            this.mapper = mapper;
            this.ratesClient = ratesClient;
            this.adapters = adapters.ToArray();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                foreach (var adapter in adapters)
                {
                    try
                    {
                        await ProcessAdapterData(adapter, stoppingToken);
                    }
                    catch (Exception exc)
                    {
                        logger.LogError(exc, $"Worker exception. Adapter: '{adapter.Name}'");
                    }

                    await Task.Delay(1000, stoppingToken);
                }

                await Task.Delay(workerSettings.JobFrequencySeconds * 1000, stoppingToken);
            }
        }

        private async Task ProcessAdapterData(IRatesAdapter adapter, CancellationToken stoppingToken)
        {
            var snapshot = await adapter.Get(stoppingToken);
            var message = mapper.Map<RatesSnapshot>(snapshot);
            await ratesClient.AddRatesSnapshotAsync(message);
        }
    }
}