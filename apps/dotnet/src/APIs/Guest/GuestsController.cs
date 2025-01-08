using Microsoft.AspNetCore.Mvc;

namespace Dotnet.APIs;

[ApiController()]
public class GuestsController : GuestsControllerBase
{
    public GuestsController(IGuestsService service)
        : base(service) { }
}
