using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BCP.RecoveryOptionPriorities;

public class GetRecoveryOptionPriorityQuery : IQuery<Result<GetRecoveryOptionPriorityQueryResult>>
{
    public long Id { get; set; }
}