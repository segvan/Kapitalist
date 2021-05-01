using System;
using System.Threading.Tasks;
using Kapitalist.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Kapitalist.RatesApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Log.Logger = SerilogLoggerFactory.CreateLogger();

            try
            {
                Log.Information($"Starting ratesApi. Environment: '{EnvironmentHelpers.GetEnvironmentName()}'");
                await CreateHostBuilder(args).Build().RunAsync();
            }
            catch (Exception exc)
            {
                Log.Fatal(exc, "Error during ratesApi startup.");
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
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(options =>
                    {
                        options.ListenAnyIP(5010, listenOptions =>
                            listenOptions.Protocols = HttpProtocols.Http1);

                        options.ListenAnyIP(5011, listenOptions =>
                            listenOptions.Protocols = HttpProtocols.Http2);
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}