namespace ObservabilityDemo.AppHost.Aspirations.Grafana;

internal static class GrafanaBuilderExtensions
{
	public static IResourceBuilder<GrafanaResource> AddGrafana(
		this IDistributedApplicationBuilder builder,
		string name,
		string? plugins = null)
	{
		var grafanaResource = new GrafanaResource(name);

		var grafanaResourceBuilder = builder.AddResource(grafanaResource)
			.WithImage("grafana/grafana")
			.WithHttpEndpoint(targetPort: 3000, name: GrafanaResource.HttpEndpointName)
			.WithHttpHealthCheck("/api/health");

		if (!string.IsNullOrEmpty(plugins))
		{
			grafanaResourceBuilder.WithEnvironment("GF_INSTALL_PLUGINS", plugins);
		}

		return grafanaResourceBuilder;
	}

	public static IResourceBuilder<GrafanaResource> WithConfig(
		this IResourceBuilder<GrafanaResource> builder,
		string source)
	{
		return builder.WithBindMount(source, "/etc/grafana", isReadOnly: true);
	}

	public static IResourceBuilder<GrafanaResource> WithDashboards(
		this IResourceBuilder<GrafanaResource> builder,
		string source)
	{
		return builder.WithBindMount(source, "/var/lib/grafana/dashboards", isReadOnly: true);
	}
}