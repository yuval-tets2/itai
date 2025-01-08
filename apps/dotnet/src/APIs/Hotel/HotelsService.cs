using Dotnet.Infrastructure;

namespace Dotnet.APIs;

public class HotelsService : HotelsServiceBase
{
    public HotelsService(DotnetDbContext context)
        : base(context) { }
}
