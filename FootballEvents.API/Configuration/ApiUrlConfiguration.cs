using FootballEvents.Application.Abstractions.Settings;
using FootballEvents.Application.Extensions;

namespace FootballEvents.API.Configuration;

public static class ApiUrlConfiguration
{
    public static IServiceCollection AddCustomApiUrl(this IServiceCollection services)
    {
        services.AddSettings<ApiUrlSettings>();

        return services;
    }
}
