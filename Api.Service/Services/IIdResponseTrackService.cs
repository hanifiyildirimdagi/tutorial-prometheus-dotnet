using Common;

namespace Api.Service.Services;

public interface IIdResponseTrackService
{
    void Add(IdGeneratorResponse response);
    List<string>? Query(string process);
}

public class IdResponseTrackService : IIdResponseTrackService
{
    private readonly List<IdGeneratorResponse> _list = new();
    public void Add(IdGeneratorResponse response)
    {
        _list.Add(response);
    }

    public List<string>? Query(string process)
    {
        return _list.FirstOrDefault(x => x.Process == process).Result ?? null;
    }
}