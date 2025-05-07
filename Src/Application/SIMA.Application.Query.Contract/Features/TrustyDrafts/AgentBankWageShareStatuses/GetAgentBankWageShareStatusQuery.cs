using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.AgentBankWageShareStatuses;

public class GetAgentBankWageShareStatusQuery : IQuery<Result<GetAgentBankWageShareStatusQueryResult>>
{
    public long Id { get; set; }
}
