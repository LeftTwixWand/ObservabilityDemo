namespace ObservabilityDemo.AppHost.Aspirations.Grafana;

internal sealed class GrafanaResource : ContainerResource
{
	internal const string HttpEndpointName = "http";

	public GrafanaResource(string name) : base(name)
	{
		HttpEndpoint = new(this, HttpEndpointName);
	}

	public EndpointReference HttpEndpoint { get; }
}