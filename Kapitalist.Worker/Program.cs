using System;
using System.Threading.Tasks;
using Kapitalist.Common;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Kapitalist.Worker
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Log.Logger = SerilogLoggerFactory.CreateLogger();

            try
            {
                Log.Information($"Starting worker. Environment: '{EnvironmentHelpers.GetEnvironmentName()}'");
                await CreateHostBuilder(args).Build().RunAsync();
            }
            catch (Exception exc)
            {
                Log.Fatal(exc, "Error during worker startup.");
                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureServices((hostContext, services) => { services.AddWorker(hostContext); });
    }
}