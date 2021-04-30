using Kapitalist.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Kapitalist.UserManagement
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Log.Logger = SerilogLoggerFactory.CreateLogger();
            
            try
            {
                Log.Information($"Starting user-management. Environment: '{EnvironmentHelpers.GetEnvironmentName()}'");
                using IHost host = CreateHostBuilder(args).Build();
                await host.RunAsync();
            }
            catch (Exception exc)
            {
                Log.Fatal(exc, "Error during user-management startup.");
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
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
