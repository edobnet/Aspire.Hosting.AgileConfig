using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Aspire.AgileConfig.Client
{
    public static class AspireAgileConfigExtensions
    {
        public static IHostBuilder UseAspireAgileConfig(this IHostBuilder builder)
        {
            var envConfiguration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            var aspireEndpoint = envConfiguration.GetConnectionString("AgileConfig");

            if (!string.IsNullOrEmpty(aspireEndpoint))
            {
                Environment.SetEnvironmentVariable("AgileConfig:nodes", aspireEndpoint);
            }

            builder.UseAgileConfig();

            return builder;
        }
    }
}
