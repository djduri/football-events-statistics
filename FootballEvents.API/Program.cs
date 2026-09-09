using Microsoft.AspNetCore.HttpOverrides;
using FootballEvents.API.Configuration;
using FootballEvents.Application;
using FootballEvents.Infrastructure;

namespace FootballEvents.API;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Zbiorcza konfiguracja us³ug
        ConfigureServices(builder);

        var app = builder.Build();

        // Konfiguracja aplikacji
        ConfigureApp(app);

        app.Run();
    }

    // Jedna metoda odpowiedzialna za konfiguracjê wszystkich us³ug
    private static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Host.UseCustomLogger(builder.Services);

        builder.Services.AddCustomExceptionHandlers();

        builder.Services.AddCustomControllers();

        builder.Services.AddCustomCors(builder.Configuration);

        builder.Services.AddCustomSwagger();

        builder.Services.AddHttpClient();

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddCustomCaching();

        builder.Services.AddApplicationLayer(builder.Configuration);

        builder.Services.AddInfrastructureLayer(builder.Configuration);

        builder.Services.AddCustomRateLimiter();
    }

    // Metoda konfiguruj¹ca aplikacjê
    private static void ConfigureApp(WebApplication app)
    {
        // Forwarded headers (X-Forwarded-For) — wa¿ne dla Docker / proxy
        var options = new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        };
        // Zezwól na wszystkie sieci / proxy, przy Dockerze czêsto potrzebne
        options.KnownNetworks.Clear();
        options.KnownProxies.Clear();
        app.UseForwardedHeaders(options);

        app.UseCustomExceptionHandlers();
        app.UseInfrastructureLayer(app.Services);
        app.UseCustomSwagger(app.Environment);
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCustomCors();
        app.UseCustomLogger();
        app.UseAuthorization();
        app.UseCustomControllers();
        app.UseCustomRateLimiter();
    }
}

