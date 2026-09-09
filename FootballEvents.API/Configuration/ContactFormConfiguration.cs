using FootballEvents.Application.Abstractions.Settings;
using FootballEvents.Application.Extensions;

namespace FootballEvents.API.Configuration;

public static class ContactFormConfiguration
{
    public static IServiceCollection AddCustomContactForm(this IServiceCollection services)
    {
        services.AddSettings<ContactFormSettings>();

        return services;
    }
}
