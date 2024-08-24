using Microsoft.AspNetCore.Mvc;

namespace Example.APIs.Ctrls.Ctrls;

[Route("api/[controller]")]
[ApiController]
public class Hello : ControllerBase
{
    [HttpGet("{name?}")]
    public IEnumerable<string> Get(
        string? name = "world",
        [FromQuery(Name = "cnt")] int count = 1,
        [FromQuery(Name = "prefix")] string prefix = "Hello"
    ) =>
        Enumerable
            .Range(1, count)
            .Select(_ => $"{prefix} {name}, at {DateTime.Now}");
}
