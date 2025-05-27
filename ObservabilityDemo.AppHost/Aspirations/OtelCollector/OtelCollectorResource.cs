namespace ObservabilityDemo.AppHost.Aspirations.OtelCollector;

internal sealed class OtelCollectorResource : ContainerResource
{
	internal const string HttpEndpointName = "otlp-http";
	internal const string GrpcEndpointName = "otlp-grpc";

	public OtelCollectorResource(string name) : base(name)
	{
		HttpEndpoint = new(this, HttpEndpointName);
		GrpcEndpoint = new(this, GrpcEndpointName);
	}

	public EndpointReference HttpEndpoint { get; }
	public EndpointReference GrpcEndpoint { get; }
}

internal static class OtelCollectorBuilderExtensions
{
	public static IResourceBuilder<OtelCollectorResource> AddOtelCollector(
		this IDistributedApplicationBuilder builder,
		string name)
	{
		var collector = new OtelCollectorResource(name);

		return builder.AddResource(collector)
			.WithImage("otel/opentelemetry-collector-contrib")
			.WithContainerName("otel-collector")
			.WithHttpEndpoint(port: 4318, targetPort: 4318, name: OtelCollectorResource.HttpEndpointName)
			.WithEndpoint(port: 4317, targetPort: 4317, name: OtelCollectorResource.GrpcEndpointName)
			.WithEndpoint(port: 9464, targetPort: 9464, name: "prometheus-metrics");
	}

	public static IResourceBuilder<OtelCollectorResource> WithConfig(
		this IResourceBuilder<OtelCollectorResource> builder,
		string source)
	{
		return builder.WithBindMount(source, "/etc/otelcol/custom-config.yaml", isReadOnly: true)
			.WithArgs("--config=/etc/otelcol/custom-config.yaml");
	}
}