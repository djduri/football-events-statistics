using FootballEvents.API.Configuration;
using FootballEvents.Application;
using FootballEvents.Infrastructure;

namespace FootballEvents.API;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigureServices(builder);

        var app = builder.Build();

        ConfigureApp(app);

        app.Run();
    }

    // Jedna metoda odpowiedzialna za konfiguracjê wszystkich us³ug
    private static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Host.UseCustomLogger();

        builder.Services.AddCustomExceptionHandlers();
        builder.Services.AddCustomControllers();
        builder.Services.AddCustomSwagger();

        builder.Services.AddApplicationLayer(builder.Configuration);
        builder.Services.AddInfrastructureLayer(builder.Configuration);
    }

    // Metoda konfiguruj¹ca aplikacjê
    private static void ConfigureApp(WebApplication app)
    {
        app.UseCustomExceptionHandlers();
        app.UseCustomLogger();
        app.UseInfrastructureLayer(app.Services);
        app.UseCustomSwagger(app.Environment);
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCustomControllers();
    }
}

