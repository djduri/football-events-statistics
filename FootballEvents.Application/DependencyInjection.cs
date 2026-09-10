using FluentValidation;
using FootballEvents.Application.Abstractions;
using FootballEvents.Application.Pipelines;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("FootballEvents.Application.IntegrationTests")]

namespace FootballEvents.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.Scan(x => x.FromAssemblyOf<Assembly>()
            .AddClasses(y => y.AssignableTo<IScopedAppService>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(x => x.FromAssemblyOf<Assembly>()
            .AddClasses(y => y.AssignableTo<ISingletonAppService>())
            .AsImplementedInterfaces()
            .WithSingletonLifetime());

        services.AddValidatorsFromAssemblyContaining<Assembly>();

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining<Assembly>();
			config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
		});

        return services;
    }
}

public sealed class Assembly
{
}
