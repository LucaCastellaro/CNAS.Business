using CNAS.Business.Models;
using MediatR.Pipeline;
using Serilog;

namespace CNAS.Business.Handlers;

/// <summary>
/// Global exception handler.
/// </summary>
/// <typeparam name="TRequest">The type of the request.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
/// <typeparam name="TException">The type of the exception.</typeparam>
public sealed class ExceptionHandler<TRequest, TResponse, TException> : IRequestExceptionHandler<TRequest, TResponse, TException>
    where TRequest : BaseRequest<TResponse>
    where TResponse : BaseError
    where TException : Exception
{
    private readonly ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionHandler{TRequest, TResponse, TException}"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    public ExceptionHandler(ILogger logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Handles the exception.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="exception">The exception.</param>
    /// <param name="state">The state.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns><see cref="Task"/></returns>
    public Task Handle(TRequest request, TException exception, RequestExceptionHandlerState<TResponse> state,
        CancellationToken cancellationToken)
    {
        _logger.Fatal(exception, "Something went wrong while handling request of type {requestType}", typeof(TRequest).Name);
        var response = new BaseError
        {
            Errors = [exception.Message],
            TotalCount = 1
        };
        state.SetHandled((TResponse)response);
        return Task.CompletedTask;
    }
}