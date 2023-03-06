using Common;
using Worker.Service.Services;
using Worker.Service.Subscribers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IPusherService, PusherService>();
builder.Services.AddTransient<IIdGeneratorRequestSubscriber,IdGeneratorRequestSubscriber>();
builder.Services.AddMetricPusher("WorkerService");
builder.Services.AddCap(x =>
{
    x.UseInMemoryStorage();
    x.UseRabbitMQ(options =>
    {
        options.Password = "admin";
        options.UserName = "admin";
        options.HostName = "localhost";
        options.VirtualHost = "/";
    });
   
});
var app = builder.Build();

app.Run();