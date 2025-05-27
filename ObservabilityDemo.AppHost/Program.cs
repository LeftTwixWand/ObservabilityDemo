using ObservabilityDemo.AppHost.Aspirations.Grafana;
using ObservabilityDemo.AppHost.Aspirations.OtelCollector;
using ObservabilityDemo.AppHost.Aspirations.Prometheus;

var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.ObservabilityDemo_ApiService>("apiservice");

builder.AddProject<Projects.ObservabilityDemo_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();

#region Database
//var db = builder.AddPostgres("pgsql").AddDatabase("weatherdb");
//.WithReference(db);
#endregion

#region Function
//builder.AddAzureFunctionsProject<Projects.ObservabilityDemo_FunctionApp>("functionapp");
#endregion

#region Grafana
//builder.AddOtelCollector("otel-collector")
//    .WithConfig("Telemetry/custom-config.yaml");

//builder.AddGrafana("grafana")
//    .WithConfig("Grafana/config")
//    .WithDashboards("Grafana/dashboards");

//builder.AddPrometheus("prometheus")
//    .WithConfig("Prometheus");
#endregion