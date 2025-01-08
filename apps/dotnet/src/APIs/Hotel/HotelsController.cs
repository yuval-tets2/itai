using Microsoft.AspNetCore.Mvc;

namespace Dotnet.APIs;

[ApiController()]
public class HotelsController : HotelsControllerBase
{
    public HotelsController(IHotelsService service)
        : base(service) { }
}
