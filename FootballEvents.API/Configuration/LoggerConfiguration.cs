using Serilog;

namespace FootballEvents.API.Configuration;

public static class LoggerExtensions
{
    public static IHostBuilder UseCustomLogger(this IHostBuilder builder)
    {
        builder.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration));

        return builder;
    }

    public static IApplicationBuilder UseCustomLogger(this IApplicationBuilder app)
    {
        app.UseSerilogRequestLogging();

        return app;
    }
}