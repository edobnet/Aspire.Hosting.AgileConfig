using Aspire.Hosting.AgileConfig;

var builder = DistributedApplication.CreateBuilder(args);

var agileConfig = builder.AddAgileConfig();

var agileConfig_apiservice = agileConfig.AddApp("apiservice", "");
var agileConfig_webfrontend = agileConfig.AddApp("webfrontend", "");


var apiService = builder.AddProject<Projects.AspireProjectWithAgileConfig_ApiService>("apiservice");
var webFrontend = builder.AddProject<Projects.AspireProjectWithAgileConfig_Web>("webfrontend").WithExternalHttpEndpoints();

apiService.WithReference(agileConfig_apiservice);
apiService.WaitFor(agileConfig);

webFrontend.WithReference(agileConfig_webfrontend);
webFrontend.WaitFor(agileConfig);

webFrontend.WithReference(apiService);
webFrontend.WaitFor(apiService);

builder.Build().Run();
