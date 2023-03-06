using Api.Service.Services;
using Common;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;

namespace Api.Service.Controllers;

[Route("[controller]")]
public class GeneratorController : Controller
{
    private readonly ICapPublisher _capPublisher;
    private readonly IIdResponseTrackService _trackService;

    public GeneratorController(ICapPublisher capPublisher, IIdResponseTrackService trackService)
    {
        _capPublisher = capPublisher;
        _trackService = trackService;
    }

    [HttpGet("id-generator")]
    public async Task<IActionResult> Generate([FromQuery] int count)
    {
        var process = Guid.NewGuid().ToString("N");
        await _capPublisher.PublishAsync("id.generator.request", new IdGeneratorRequest(count, process));
        return Ok($"Process started. You can track with {process}");
    }
    
    [HttpGet("query/{process}")]
    public IActionResult Query(string process)
    {
        return Ok(_trackService.Query(process));
    }
}