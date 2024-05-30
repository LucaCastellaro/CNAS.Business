using MediatR;

namespace CNAS.Business.Models;

/// <summary>
/// The base class for creating a Response.
/// </summary>
/// <typeparam name="TResponse">The type of the response.</typeparam>
/// <seealso cref="MediatR.IRequest&lt;TResponse&gt;" />
/// <seealso cref="MediatR.IBaseRequest" />
/// <seealso cref="System.IEquatable&lt;CNAS.Business.Models.BaseRequest&lt;TResponse&gt;&gt;" />
public record BaseRequest<TResponse> : IRequest<TResponse>;