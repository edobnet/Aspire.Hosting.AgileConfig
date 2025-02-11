using Aspire.Hosting.ApplicationModel;
using System;

namespace Aspire.Hosting.AgileConfig
{
    public static class AgileConfigBuilderExtension
    {
        public static IResourceBuilder<AgileConfigResource> AddAgileConfig(this IDistributedApplicationBuilder builder, [ResourceName] string name,
            int? port = null)
        {
            ArgumentNullException.ThrowIfNull(builder);
            ArgumentNullException.ThrowIfNull(name);

            var agileConfigResource = new AgileConfigResource(name);

            return builder.AddResource(agileConfigResource)
                .WithEndpoint(port: port, targetPort: 5000, name: AgileConfigResource.PrimaryEndpointName)
                .WithImage(ImageTags.Image, ImageTags.Tag)
                .WithImageRegistry(ImageTags.Registry);
        }
    }
}
