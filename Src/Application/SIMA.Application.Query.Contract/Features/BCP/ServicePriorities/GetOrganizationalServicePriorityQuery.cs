using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BCP.ServicePriorities;

public class GetOrganizationalServicePriorityQuery : IQuery<Result<GetOrganizationalServicePriorityQueryResult>>
{
    public long Id { get; set; }
}