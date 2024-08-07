using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Logistics.Cartables;

public class GetMyLogisticCartableGetQuery : IQuery<Result<GetMyLogisticCartableGetQueryResult>>
{
    public long Id { get; set; }
    public long IssueId { get; set; }
}
