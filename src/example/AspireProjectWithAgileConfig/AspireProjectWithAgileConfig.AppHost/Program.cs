using Aspire.Hosting.AgileConfig;

var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var agileConfig = builder.AddAgileConfig();

var apiService = builder.AddProject<Projects.AspireProjectWithAgileConfig_ApiService>("apiservice");

builder.AddProject<Projects.AspireProjectWithAgileConfig_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(agileConfig)
    .WaitFor(agileConfig)
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
