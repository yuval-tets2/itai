using Dotnet.APIs;

namespace Dotnet;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IGuestsService, GuestsService>();
        services.AddScoped<IHotelsService, HotelsService>();
    }
}
