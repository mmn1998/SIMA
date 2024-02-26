using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Locations;

public class GetLocationQuery : IQuery<Result<GetLocationQueryResult>>
{
    public long Id { get; set; }
}
