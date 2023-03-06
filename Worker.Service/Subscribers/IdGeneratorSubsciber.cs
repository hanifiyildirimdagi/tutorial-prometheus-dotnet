using Common;
using DotNetCore.CAP;
using Worker.Service.Services;

namespace Worker.Service.Subscribers;

public interface IIdGeneratorRequestSubscriber
{
    Task CheckReceivedMessage(IdGeneratorRequest request);
}

public class IdGeneratorRequestSubscriber : IIdGeneratorRequestSubscriber, ICapSubscribe
{
    private readonly IPusherService _pusherService;

    public IdGeneratorRequestSubscriber(IPusherService pusherService)
    {
        _pusherService = pusherService;
    }

    [CapSubscribe("id.generator.request")]
    public async Task CheckReceivedMessage(IdGeneratorRequest request)
    {
        var result = new List<string>();
        for (int i = 0; i < request.Count; i++)
        {   
            result.Add(Guid.NewGuid().ToString("N"));
        }

        await _pusherService.PushIdGeneratorResponse(new IdGeneratorResponse(result, request.Process));
    }
}