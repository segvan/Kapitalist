using System.IO;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Kapitalist.Common
{
    public static class SerilogLoggerFactory
    {
        public static ILogger CreateLogger()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddEnvironmentVariables();

            string envName = EnvironmentHelpers.GetEnvironmentName();
            if (!string.IsNullOrWhiteSpace(envName))
            {
                builder.AddJsonFile($"appsettings.{envName}.json", optional: true);
            }

            IConfigurationRoot configuration = builder.Build();

            return new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }
    }
}