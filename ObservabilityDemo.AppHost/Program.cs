var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");
var db = builder.AddPostgres("pgsql").AddDatabase("weatherdb");

var apiService = builder.AddProject<Projects.ObservabilityDemo_ApiService>("apiservice")
    .WithReference(db);

builder.AddProject<Projects.ObservabilityDemo_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
