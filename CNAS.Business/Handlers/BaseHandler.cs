using MediatR;
using Serilog;

namespace CNAS.Business.Handlers;

/// <summary>
/// The base class for creating a Handler.
/// </summary>
/// <typeparam name="TRequest">The type of the request.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
/// <seealso cref="MediatR.IRequestHandler&lt;TRequest, TResponse&gt;" />
public abstract class BaseHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    /// <summary>
    /// The logger.
    /// </summary>
    protected readonly ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseHandler{TRequest, TResponse}"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    protected BaseHandler(ILogger logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Handles the specified request.
    /// </summary>
    /// <param name="req">The request.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns><c>Task<TResponse></c></returns>
    public abstract Task<TResponse> Handle(TRequest req, CancellationToken ct);
}