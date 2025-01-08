using Dotnet.Infrastructure;

namespace Dotnet.APIs;

public class GuestsService : GuestsServiceBase
{
    public GuestsService(DotnetDbContext context)
        : base(context) { }
}
