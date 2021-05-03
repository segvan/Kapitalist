using System;
using System.Threading.Tasks;
using Kapitalist.Common;
using Kapitalist.RatesApi.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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

                var host = CreateHostBuilder(args).Build();

                using (var scope = host.Services.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<RatesDataContext>();
                    await context.Database.MigrateAsync();
                }

                await host.RunAsync();
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