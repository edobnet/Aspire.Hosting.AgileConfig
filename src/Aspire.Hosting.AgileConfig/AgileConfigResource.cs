using Aspire.Hosting.ApplicationModel;

namespace Aspire.Hosting.AgileConfig
{
    public class AgileConfigResource(string name) : ContainerResource(name), IResourceWithConnectionString
    {
        internal const string PrimaryEndpointName = "http";
        internal const string InternalEndpointName = "internal";

        private EndpointReference _primaryEndpoint;
        private EndpointReference _internalEndpoint;

        /// <summary>
        /// Gets the primary endpoint for the Elasticsearch. This endpoint is used for all API calls over HTTP.
        /// </summary>
        public EndpointReference PrimaryEndpoint => _primaryEndpoint ??= new(this, PrimaryEndpointName);

        /// <summary>
        /// Gets the internal endpoint for the Elasticsearch. This endpoint used for communications between nodes in a cluster
        /// </summary>
        public EndpointReference InternalEndpoint => _internalEndpoint ??= new(this, InternalEndpointName);

        public ReferenceExpression ConnectionStringExpression =>
            ReferenceExpression.Create($"http://{PrimaryEndpoint.Property(EndpointProperty.Host)}:{PrimaryEndpoint.Property(EndpointProperty.Port)}");

    }
}
