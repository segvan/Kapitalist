using System;
using System.Threading.Tasks;
using Kapitalist.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Kapitalist.Bot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Log.Logger = SerilogLoggerFactory.CreateLogger();

            try
            {
                Log.Information($"Starting bot. Environment: '{EnvironmentHelpers.GetEnvironmentName()}'");
                await CreateHostBuilder(args).Build().RunAsync();
            }
            catch (Exception exc)
            {
                Log.Fatal(exc, "Error during bot startup.");
                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureServices((hostContext, services) => { services.AddHostedService<Startup>(); });
    }
}