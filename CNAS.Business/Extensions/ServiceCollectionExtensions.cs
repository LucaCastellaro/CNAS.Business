using CNAS.Business.Behaviors;
using CNAS.Business.Handlers;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;

namespace CNAS.Business.Extensions;

/// <summary>
/// Extension methods to add Business in your program.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add mediator pattern.
    /// </summary>
    /// <typeparam name="TMarker">The marker.</typeparam>
    /// <param name="services">The services.</param>
    /// <returns>The <see cref="IServiceCollection" /> so that other methods can be chained.</returns>
    public static IServiceCollection AddMediator<TMarker>(this IServiceCollection services)
    {
        return services.AddMediatR(xx =>
        {
            xx.RegisterServicesFromAssembly(typeof(TMarker).Assembly);
        });
    }

    /// <summary>
    /// Add behaviors to the pipeline.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns>The <see cref="IServiceCollection" /> so that other methods can be chained.</returns>
    public static IServiceCollection AddBehaviors(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }

    /// <summary>
    /// Add validators to the pipeline.
    /// </summary>
    /// <typeparam name="TMarker">The marker.</typeparam>
    /// <param name="services">The services.</param>
    /// <returns>The <see cref="IServiceCollection" /> so that other methods can be chained.</returns>
    public static IServiceCollection AddValidators<TMarker>(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(TMarker).Assembly,
            ServiceLifetime.Transient,
            filter: null,
            includeInternalTypes: true);

        return services;
    }

    /// <summary>
    /// Adds the <see cref="ExceptionHandler{TRequest, TResponse, TException}" />.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns>The <see cref="IServiceCollection" /> so that other methods can be chained.</returns>
    public static IServiceCollection AddExceptionHandler(this IServiceCollection services)
    {
        services.AddTransient(typeof(IRequestExceptionHandler<,,>), typeof(ExceptionHandler<,,>));
        return services;
    }
}