using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Profiles;

public class GetProfileQuery : IQuery<Result<GetProfileQueryResult>>
{
    public long Id { get; set; }
}
