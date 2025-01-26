using SIMA.Application.Query.Contract.Features.RiskManagement.EvaluationCriterias;
using SIMA.Application.Query.Contract.Features.RiskManagement.TriggerStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Features.RiskManagement.TriggerStatuses;

public class TriggerStatusQueryHandler: IQueryHandler<GetAllTriggerStatusesQuery, Result<IEnumerable<GetTriggerStatusesQueryResult>>>,
    IQueryHandler<GetTriggerStatusesQuery, Result<GetTriggerStatusesQueryResult>>

{
   // private readonly ITriggerStatusRepository _repository;

    
    public Task<Result<IEnumerable<GetTriggerStatusesQueryResult>>> Handle(GetAllTriggerStatusesQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result<GetTriggerStatusesQueryResult>> Handle(GetTriggerStatusesQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}