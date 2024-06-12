using SIMA.Application.Query.Contract.Features.BCP.RecoveryPointObjectives;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.BCP.Consequences;

namespace SIMA.Application.Query.Features.BCP.RecoveryPointObjectives;

public class RecoveryPointObjectiveQueryHandler : IQueryHandler<GetRecoveryPointObjectiveQuery, Result<GetRecoveryPointObjectiveQueryResult>>,
    IQueryHandler<GetAllRecoveryPointObjectivesQuery, Result<IEnumerable<GetRecoveryPointObjectiveQueryResult>>>
{
    private readonly IRecoveryPointObjectiveQueryRepository _repository;

    public RecoveryPointObjectiveQueryHandler(IRecoveryPointObjectiveQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetRecoveryPointObjectiveQueryResult>> Handle(GetRecoveryPointObjectiveQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetRecoveryPointObjectiveQueryResult>>> Handle(GetAllRecoveryPointObjectivesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}