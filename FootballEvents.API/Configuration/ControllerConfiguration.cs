using System.Text.Json.Serialization;

namespace FootballEvents.API.Configuration;

public static class ControllerConfiguration
{
    public static IServiceCollection AddCustomControllers(this IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        return services;
    }

    public static IApplicationBuilder UseCustomControllers(this WebApplication app)
    {
        app.MapControllers();

        return app;
    }
}