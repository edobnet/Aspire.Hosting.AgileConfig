using Aspire.Hosting.ApplicationModel;
using System;
using System.Linq;

namespace Aspire.Hosting.AgileConfig
{
    public static class AgileConfigBuilderExtension
    {
        private const string SqliteConnString = "Data Source=agile_config.db";

        public static IResourceBuilder<AgileConfigResource> AddAgileConfig(this IDistributedApplicationBuilder builder, [ResourceName] string name = "AgileConfig", string tag= ImageTags.Tag,
            int? port = null,int? targetPort= 5000, AgileConfigDbProvider provider = AgileConfigDbProvider.sqlite, string connString= SqliteConnString)
        {
            ArgumentNullException.ThrowIfNull(builder);
            ArgumentNullException.ThrowIfNull(name);

            var agileConfigResource = new AgileConfigResource(name);

            return builder.AddResource(agileConfigResource)
                .WithEndpoint(port: port, targetPort: targetPort, name: AgileConfigResource.InternalEndpointName)
                .WithHttpEndpoint(port: port, targetPort: targetPort, name: AgileConfigResource.PrimaryEndpointName)
                .WithImage(ImageTags.Image, tag)
                .WithImageRegistry(ImageTags.Registry)
                .WithEnvironment(context =>
                {
                    context.EnvironmentVariables["adminConsole"] = "true";
                    context.EnvironmentVariables["db:provider"] = provider.ToString();
                    context.EnvironmentVariables["db:conn"] = connString;
                })
                ;
        }
    }
}
