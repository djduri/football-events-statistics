using FootballEvents.Application.Abstractions.Settings;
using FootballEvents.Application.Extensions;

namespace FootballEvents.API.Configuration;

public static class BrandingConfiguration
{
    public static IServiceCollection AddCustomBranding(this IServiceCollection services)
    {
        services.AddSettings<BrandingSettings>();

        return services;
    }
}
