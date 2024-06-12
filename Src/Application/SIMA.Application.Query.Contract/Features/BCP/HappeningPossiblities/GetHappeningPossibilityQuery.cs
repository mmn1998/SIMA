using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BCP.HappeningPossiblities;

public class GetHappeningPossibilityQuery : IQuery<Result<GetHappeningPossibilityQueryResult>>
{
    public long Id { get; set; }
}