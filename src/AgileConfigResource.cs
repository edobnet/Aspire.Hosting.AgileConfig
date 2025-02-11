using Aspire.Hosting.ApplicationModel;
namespace Aspire.Hosting.ApplicationModel
{
    public class AgileConfigResource(string name) : ContainerResource(name)
    {
        internal const string PrimaryEndpointName = "tcp";
    }
}
