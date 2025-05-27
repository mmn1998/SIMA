using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.AccessTypes;

public class GetAccessTypeQuery : IQuery<Result<GetAccessTypeQueryResult>>
{
    public long Id { get; set; }
}
