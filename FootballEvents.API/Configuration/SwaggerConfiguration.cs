using Microsoft.OpenApi.Models;

namespace FootballEvents.API.Configuration;

public static class SwaggerConfiguration
{
	public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
	{
		services.AddEndpointsApiExplorer();

		services.AddSwaggerGen(config =>
		{	
			config.SwaggerDoc("Cms", new OpenApiInfo
			{
				Title = "Football Events API",
				Version = $"v1"
			});           

            config.SupportNonNullableReferenceTypes();
			config.NonNullableReferenceTypesAsRequired();
        });		

		return services;
	}

	public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app, IWebHostEnvironment env)
	{
		if (env.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI(config =>
			{
				config.SwaggerEndpoint("/swagger/Cms/swagger.json", "Football Events API");
				//config.SwaggerEndpoint("/swagger/Mobile%20User/swagger.json", "Mobile User");
			});
		}

		return app;
	}
}
