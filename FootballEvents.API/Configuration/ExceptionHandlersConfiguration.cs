using FootballEvents.API.Handlers;

namespace FootballEvents.API.Configuration;

public static class ExceptionHandlersConfiguration
{
	public static IServiceCollection AddCustomExceptionHandlers(this IServiceCollection services)
	{
        services.AddExceptionHandler<ValidationExceptionHandler>();
		services.AddExceptionHandler<DomainExceptionHandler>();
		services.AddExceptionHandler<UnhandledExceptionHandler>();

		services.AddProblemDetails();

		return services;
	}

	public static IApplicationBuilder UseCustomExceptionHandlers(this IApplicationBuilder app)
	{
		app.UseExceptionHandler();

		return app;
	}
}
