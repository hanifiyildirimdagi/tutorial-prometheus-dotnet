using Common;
using DotNetCore.CAP;

namespace Worker.Service.Services;

public interface IPusherService
{
    Task PushIdGeneratorResponse(IdGeneratorResponse response);
}

public class PusherService : IPusherService
{

    private readonly ICapPublisher _capPublisher;

    public PusherService(ICapPublisher capPublisher)
    {
        _capPublisher = capPublisher;
    }

    public async Task PushIdGeneratorResponse(IdGeneratorResponse response)
    {
        await _capPublisher.PublishAsync("id.generator.response", response);
    }
}