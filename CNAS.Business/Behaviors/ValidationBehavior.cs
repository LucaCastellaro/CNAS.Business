using CNAS.Business.Models;
using FluentValidation;
using MediatR;

namespace CNAS.Business.Behaviors;

/// <summary>
/// The validation behavior.
/// </summary>
/// <typeparam name="TRequest">The type of the request.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
/// <seealso cref="MediatR.IPipelineBehavior&lt;TRequest, TResponse&gt;" />
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    where TResponse : BaseError, new()
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationBehavior{TRequest, TResponse}"/> class.
    /// </summary>
    /// <param name="validators">The validators.</param>
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    /// <summary>
    /// Pipeline handler. Performs validation checks:<br />
    /// <list type="bullet">
    /// <item>
    /// <term>
    /// NO errors
    /// </term>
    /// <description>
    /// Continue with the <paramref name="next" /> operation in pipeline, eventually returning <typeparamref name="TResponse" />
    /// </description>
    /// </item>
    /// <item>
    /// <term>
    /// 1 or more errors
    /// </term>
    /// <description>
    /// Break the execution and return <typeparamref name="TResponse" /> with errors
    /// </description>
    /// </item>
    /// </list>
    /// </summary>
    /// <param name="request">Incoming request</param>
    /// <param name="next">Awaitable delegate for the next action in the pipeline. Eventually this delegate represents the handler.</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>
    /// Awaitable task returning the <typeparamref name="TResponse" />
    /// </returns>
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
    {
        var context = new ValidationContext<TRequest>(request);
        var failures = _validators
            .Select(validator => validator.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(failure => failure is not null)
            .ToList();

        if (failures.Any())
        {
            return Task.FromResult(new TResponse
            {
                Errors = failures.Select(failure => $"{failure.Severity} {failure.ErrorCode}: {failure.ErrorMessage}")
                    .ToList(),
                TotalCount = failures.Count
            });
        }

        return next();
    }
}