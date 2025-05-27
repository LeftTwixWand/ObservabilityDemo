namespace ObservabilityDemo.AppHost.Aspirations.Prometheus;

internal sealed class PrometheusResource : ContainerResource
{
	internal const string HttpEndpointName = "http";

	public PrometheusResource(string name) : base(name)
	{
		HttpEndpoint = new(this, HttpEndpointName);
	}

	public EndpointReference HttpEndpoint { get; }
}