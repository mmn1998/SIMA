using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Groups;

public class GetGroupQuery : IQuery<Result<GetGroupQueryResult>>
{
    public long Id { get; set; }
}
