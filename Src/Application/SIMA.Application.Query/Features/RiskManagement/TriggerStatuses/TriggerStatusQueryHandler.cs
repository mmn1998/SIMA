using SIMA.Application.Query.Contract.Features.RiskManagement.TriggerStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.RiskManagement.TriggerStatuses;

namespace SIMA.Application.Query.Features.RiskManagement.TriggerStatuses;

public class TriggerStatusQueryHandler: IQueryHandler<GetAllTriggerStatusesQuery, Result<IEnumerable<GetTriggerStatusesQueryResult>>>,
    IQueryHandler<GetTriggerStatusQuery, Result<GetTriggerStatusesQueryResult>>

{
   private readonly ITriggerStatusQueryRepository _repository;

   public TriggerStatusQueryHandler(ITriggerStatusQueryRepository repository)
   {
       _repository = repository;
   }


   public async Task<Result<IEnumerable<GetTriggerStatusesQueryResult>>> Handle(GetAllTriggerStatusesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }

    public async Task<Result<GetTriggerStatusesQueryResult>> Handle(GetTriggerStatusQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request.Id);
        return Result.Ok(result);
    }
}