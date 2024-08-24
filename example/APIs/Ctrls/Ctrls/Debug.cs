using Microsoft.AspNetCore.Mvc;

namespace Example.APIs.Ctrls.Ctrls;

[Route("api/[controller]")]
[ApiController]
public class Debug : ControllerBase
{
    [HttpGet]
    [AcceptVerbs("GET")]
    public List<string> ListRoutes(IEnumerable<EndpointDataSource> ds) =>
        ds.SelectMany(d => d.Endpoints).SelectMany(Fmt).ToList();

    private static List<string> Fmt(Endpoint e)
    {
        if (e is not RouteEndpoint r)
        {
            return [e.DisplayName!];
        }

        var methods =
            (
                r.Metadata.FirstOrDefault(m => m is HttpMethodMetadata)
                as HttpMethodMetadata
            )?.HttpMethods.ToArray() ?? [];

        return [$"[{string.Join(",", methods)}] {r.RoutePattern.RawText}"];
    }
}
