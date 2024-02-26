using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.LocationTypes;

public class GetLocationTypeQuery : IQuery<Result<GetLocationTypeQueryResult>>
{
    public long Id { get; set; }
}
