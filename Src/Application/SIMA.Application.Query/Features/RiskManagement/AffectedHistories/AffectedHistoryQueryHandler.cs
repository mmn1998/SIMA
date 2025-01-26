using SIMA.Application.Query.Contract.Features.RiskManagement.AffectedHistories;
using SIMA.Application.Query.Contract.Features.RiskManagement.ConsequenceCategories;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.RiskManagement.AffectedHistories;

namespace SIMA.Application.Query.Features.RiskManagement.AffectedHistories;

public class AffectedHistoryQueryHandler : IQueryHandler<GetAffectedHistoryQuery, Result<GetAffectedHistoryQueryResult>>,
    IQueryHandler<GetAllAffectedHistoriesQuery, Result<IEnumerable<GetAffectedHistoryQueryResult>>>
{
    private readonly IAffectedHistoryQueryRepository _repository;

    public AffectedHistoryQueryHandler(IAffectedHistoryQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetAffectedHistoryQueryResult>> Handle(GetAffectedHistoryQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetAffectedHistoryQueryResult>>> Handle(GetAllAffectedHistoriesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}