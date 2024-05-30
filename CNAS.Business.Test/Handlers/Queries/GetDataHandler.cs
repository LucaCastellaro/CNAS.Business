using CNAS.Business.Handlers;
using CNAS.Business.Models;
using Microsoft.Extensions.Logging;

namespace CNAS.Business.Test.Handlers.Queries;

internal sealed record GetDataResponse : BaseResponse<int>;

internal sealed record GetDataQuery : BaseRequest<GetDataResponse>
{
    public required int Value { get; init; }
    public bool? Throw { get; init; }
}

internal sealed class GetDataHandler : BaseHandler<GetDataQuery, GetDataResponse>
{
    public GetDataHandler(ILogger<GetDataHandler> logger) : base(logger)
    {
    }

    public override Task<GetDataResponse> Handle(GetDataQuery req, CancellationToken ct)
    {
        if (req.Throw == true)
        {
            throw new NotImplementedException();
        }

        return Task.FromResult(
            new GetDataResponse
            {
                Data = req.Value
            }
        );
    }
}