using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using AwladRizk.Application.Behaviors;

namespace AwladRizk.Application;

/// <summary>
/// Assembly marker + DI registration for the Application layer.
/// </summary>
public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        // MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        // AutoMapper
        services.AddAutoMapper(assembly);

        // FluentValidation
        services.AddValidatorsFromAssembly(assembly);

        // Pipeline behaviors
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
