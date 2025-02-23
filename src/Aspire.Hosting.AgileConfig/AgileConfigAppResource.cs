using Aspire.Hosting.ApplicationModel;
using System;

namespace Aspire.Hosting.AgileConfig
{
    public class AgileConfigAppResource(string appId, string appSecret, AgileConfigServerResource parent) : Resource($"AgileConfig{appId}"), IResourceWithParent<AgileConfigServerResource>, IResourceWithConnectionString
    {
        public AgileConfigServerResource Parent { get; } = parent;

        public ReferenceExpression AppIdReference => ReferenceExpression.Create($"{appId}");
        public ReferenceExpression AppSecretReference => ReferenceExpression.Create($"{appSecret}");

        public ReferenceExpression ConnectionStringExpression =>
            ReferenceExpression.Create($"{Parent};appid={appId};secret={appSecret}");
    }
}
