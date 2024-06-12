using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BCP.RecoveryPointObjectives;

public class GetRecoveryPointObjectiveQuery : IQuery<Result<GetRecoveryPointObjectiveQueryResult>>
{
    public long Id { get; set; }
}