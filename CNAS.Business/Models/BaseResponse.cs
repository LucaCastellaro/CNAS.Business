namespace CNAS.Business.Models;

/// <summary>
/// The base class for creating a Request.
/// </summary>
/// <typeparam name="TData">The type of the data.</typeparam>
/// <seealso cref="CNAS.Business.Models.BaseError" />
/// <seealso cref="System.IEquatable&lt;CNAS.Business.Models.BaseError&gt;" />
/// <seealso cref="System.IEquatable&lt;CNAS.Business.Models.BaseResponse&lt;TData&gt;&gt;" />
public record BaseResponse<TData> : BaseError
{
    /// <summary>
    /// Gets or sets the data.
    /// </summary>
    /// <value>
    /// The data.
    /// </value>
    public TData? Data { get; set; }
}