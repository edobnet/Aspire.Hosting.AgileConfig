using Aspire.Hosting.AgileConfig;

var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var agileConfig = builder.AddAgileConfig();

var agileConfig_myapp = agileConfig.AddApp("myapp", "");

var apiService = builder.AddProject<Projects.AspireProjectWithAgileConfig_ApiService>("apiservice");

builder.AddProject<Projects.AspireProjectWithAgileConfig_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)

    .WithReference(agileConfig_myapp)
    .WaitFor(agileConfig_myapp)
    .WaitFor(agileConfig)

    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
