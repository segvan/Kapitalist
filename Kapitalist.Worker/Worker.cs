using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kapitalist.Worker.Adapters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Kapitalist.Worker
{
    public class Worker : BackgroundService
    {
        private readonly WorkerSettings workerSettings;
        private readonly ILogger<Worker> logger;
        private readonly IRatesAdapter[] adapters;

        public Worker(IOptions<WorkerSettings> workerSettings, ILogger<Worker> logger,
            IEnumerable<IRatesAdapter> adapters)
        {
            this.workerSettings = workerSettings.Value;
            this.logger = logger;
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
                        var result = await adapter.Get(stoppingToken);
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
    }
}