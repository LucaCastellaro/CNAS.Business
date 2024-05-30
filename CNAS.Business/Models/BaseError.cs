namespace CNAS.Business.Models;

/// <summary>
/// The base class for accessing errors in the Response.
/// </summary>
public record BaseError
{
    public List<string> Errors { get; set; } = Enumerable.Empty<string>().ToList();
    public int TotalCount { get; set; }

    public bool HasErrors => Errors.Any();
}