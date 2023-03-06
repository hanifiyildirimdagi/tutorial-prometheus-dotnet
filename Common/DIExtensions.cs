using Microsoft.Extensions.DependencyInjection;
using Prometheus.Client.Collectors;
using Prometheus.Client.Collectors.DotNetStats;
using Prometheus.Client.Collectors.ProcessStats;
using Prometheus.Client.MetricPusher;

namespace Common;

public static class DiExtensions
{
    public static IServiceCollection AddMetricPusher(this IServiceCollection serviceCollection, string jobName,
        string? instance = null)
    {
        instance ??= Environment.GetEnvironmentVariable("HOSTNAME") ?? Guid.NewGuid().ToString();
        var reg = new CollectorRegistry();
        reg.Add(new ProcessCollector(System.Diagnostics.Process.GetCurrentProcess(), jobName));
        reg.Add(new GCTotalMemoryCollector(jobName));
        reg.Add(new GCCollectionCountCollector(jobName));
        var pusher = new MetricPusher(new MetricPusherOptions
        {
            Endpoint = "http://localhost:9091", 
            Job = jobName, 
            Instance = instance, 
            CollectorRegistry = reg
        });
        serviceCollection.AddSingleton<MetricPusher>(pusher);
        serviceCollection.AddHostedService<MetricPusherBackgroundService>();
        return serviceCollection;
    }
}