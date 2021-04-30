using System;

namespace Kapitalist.Common
{
    public static class EnvironmentHelpers
    {
        public static string GetEnvironmentName()
        {
            return !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
                ? Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                : Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
        }
    }
}