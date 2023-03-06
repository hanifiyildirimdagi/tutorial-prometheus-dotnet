using Api.Service.Services;
using Common;
using DotNetCore.CAP;

namespace Api.Service.Subscribers;

public interface IIdGeneratorResponseSubscriber
{
    Task CheckReceivedMessage(IdGeneratorResponse response);
}

public class IdGeneratorResponseSubscriber : IIdGeneratorResponseSubscriber,ICapSubscribe
{
    private readonly IIdResponseTrackService _trackService;

    public IdGeneratorResponseSubscriber(IIdResponseTrackService trackService)
    {
        _trackService = trackService;
    }

    [CapSubscribe("id.generator.response")]
    public async Task CheckReceivedMessage(IdGeneratorResponse response)
    {
        _trackService.Add(response);
        await Task.CompletedTask;
    }
}