using FootballEvents.Application.Extensions;
using Serilog;

namespace FootballEvents.API.Configuration;

public static class LoggerConfiguration
{
	public static IHostBuilder UseCustomLogger(this IHostBuilder builder, IServiceCollection services)
	{
		builder.UseSerilog((context, configuration) =>
			configuration.ReadFrom.Configuration(context.Configuration));	

		return builder;
	}

	public static IApplicationBuilder UseCustomLogger(this IApplicationBuilder app)
	{	
		app.UseSerilogRequestLogging(config =>
		{
			config.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
			{
				diagnosticContext.Set("User-Agent", httpContext.Request.Headers.UserAgent);
				diagnosticContext.Set("Accept-Language", httpContext.Request.Headers.AcceptLanguage);
				diagnosticContext.Set("IP-Address", httpContext.Request.GetIpAddress());
			};
		});

		return app;
	}
}
