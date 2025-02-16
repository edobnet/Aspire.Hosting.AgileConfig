using Aspire.Hosting.ApplicationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Routing;

namespace Aspire.Hosting.AgileConfig
{
    public static class AgileConfigBuilderExtension
    {
        public static IResourceBuilder<AgileConfigResource> AddAgileConfig(this IDistributedApplicationBuilder builder, [ResourceName] string name = "AgileConfig", string tag = ImageTags.Tag,
            int? port = null, int? targetPort = 5000, IDictionary<string, string>? environmentVariables = null)
        {
            ArgumentNullException.ThrowIfNull(builder);
            ArgumentNullException.ThrowIfNull(name);

            var agileConfigResource = new AgileConfigResource(name);

            return builder.AddResource(agileConfigResource)
                .WithEndpoint(port: port, targetPort: targetPort, name: AgileConfigResource.InternalEndpointName)
                .WithHttpEndpoint(port: port, targetPort: targetPort, name: AgileConfigResource.PrimaryEndpointName)
                .WithImage(ImageTags.Image, tag)
                .WithImageRegistry(ImageTags.Registry)
                .WithLifetime(ContainerLifetime.Persistent)
                .WithContainerName($"Aspire_AgileConfig_{tag}")
                .WithEnvironment(context =>
                {
                    // as default
                    context.EnvironmentVariables["adminConsole"] = "true";
                    context.EnvironmentVariables["db:provider"] = AgileConfigDbProvider.sqlite.ToString();
                    context.EnvironmentVariables["db:conn"] = "Data Source=agile_config.db";

                    if (environmentVariables != null)
                    {
                        foreach (var environmentVariable in environmentVariables)
                        {
                            context.EnvironmentVariables[environmentVariable.Key] = environmentVariable.Value;
                        }
                    }
                })
                .WithHttpHealthCheck("/home/echo")
                .WithOtlpExporter()
                ;
        }
    }
}
