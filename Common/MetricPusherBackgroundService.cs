using Microsoft.Extensions.Hosting;
using Prometheus.Client.MetricPusher;

namespace Common;

public class MetricPusherBackgroundService : BackgroundService
{
    private readonly MetricPusher _metricPusher;

    public MetricPusherBackgroundService(MetricPusher metricPusher)
    {
        _metricPusher = metricPusher;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var worker = new MetricPushServer(_metricPusher);
        worker.Start();
        await _metricPusher.PushAsync();
        await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
    }
}