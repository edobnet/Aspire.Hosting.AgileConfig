using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Text.RegularExpressions;

namespace Aspire.AgileConfig.Client
{
    public static class AspireAgileConfigExtensions
    {
        public static IHostBuilder UseAspireAgileConfig(this IHostBuilder builder, string appId)
        {
            var envConfiguration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            var connectionString = envConfiguration.GetConnectionString($"AgileConfig{appId}");
            var connection = ParseConnectionString(connectionString);

            if (!string.IsNullOrEmpty(connectionString))
            {
                Environment.SetEnvironmentVariable("AgileConfig:nodes", connection.Nodes);
                Environment.SetEnvironmentVariable("AgileConfig:appId", connection.AppId);
                Environment.SetEnvironmentVariable("AgileConfig:secret", connection.Secret);
            }

            builder.UseAgileConfig();

            return builder;
        }

        private static readonly Regex PairRegex = new Regex(
            @"(?<key>[^=]+)=(?<value>.*?)(?=(;\s*[^=]+=)|;?$)",
            RegexOptions.Compiled | RegexOptions.IgnoreCase
        );


        private static ConnectionInfo ParseConnectionString(string connectionString)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(connectionString);

            var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                foreach (Match match in PairRegex.Matches(connectionString))
                {
                    if (!match.Success) continue;

                    var key = match.Groups["key"].Value.Trim();
                    var value = match.Groups["value"].Value.Trim();

                    if (!string.IsNullOrEmpty(key))
                    {
                        dict[key] = value;
                    }
                }
            }

            return new ConnectionInfo
            {
                Nodes = GetValueOrDefault(dict, "nodes"),
                AppId = GetValueOrDefault(dict, ";appid"),
                Secret = GetValueOrDefault(dict, ";secret")
            };
        }

        private static string GetValueOrDefault(IDictionary<string, string> dict, string key)
        {
            return dict.TryGetValue(key, out var value) ? value : "";
        }

        private class ConnectionInfo
        {
            public string? Nodes { get; set; }
            public string? AppId { get; set; }
            public string? Secret { get; set; }
        }
    }
}
