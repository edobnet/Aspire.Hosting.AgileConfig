using Aspire.Hosting.AgileConfig;

var builder = DistributedApplication.CreateBuilder(args);

var agc = builder.AddAgileConfig();

builder.Build().Run();
