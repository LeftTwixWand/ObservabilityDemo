namespace ObservabilityDemo.AppHost.Aspirations.Prometheus;

internal static class PrometheusBuilderExtensions
{
	public static IResourceBuilder<PrometheusResource> AddPrometheus(this IDistributedApplicationBuilder builder, string name)
	{
		var prometheus = new PrometheusResource(name);

		return builder.AddResource(prometheus)
			.WithImage("prom/prometheus")
			.WithHttpEndpoint(port: 9090, targetPort: 9090, name: PrometheusResource.HttpEndpointName)
			.WithHttpHealthCheck("/-/healthy");
	}

	public static IResourceBuilder<PrometheusResource> WithConfig(this IResourceBuilder<PrometheusResource> builder, string source)
	{
		return builder.WithBindMount(source, "/etc/prometheus", isReadOnly: true);
	}
}