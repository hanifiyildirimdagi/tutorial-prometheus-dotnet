using Api.Service.Services;
using Api.Service.Subscribers;
using Common;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IIdResponseTrackService, IdResponseTrackService>();
builder.Services.AddTransient<IIdGeneratorResponseSubscriber, IdGeneratorResponseSubscriber>();
builder.Services.AddMetricPusher("ApiService");
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


app.MapControllers();

app.Run();