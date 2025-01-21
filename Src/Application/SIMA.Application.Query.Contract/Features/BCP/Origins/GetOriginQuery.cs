using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BCP.Origins;

public class GetOriginQuery : IQuery<Result<GetOriginQueryResult>>
{
    public long Id { get; set; }
}