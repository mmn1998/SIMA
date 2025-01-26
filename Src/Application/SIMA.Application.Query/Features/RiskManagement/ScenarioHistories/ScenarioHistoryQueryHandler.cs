using SIMA.Application.Query.Contract.Features.RiskManagement.Risks;
using SIMA.Application.Query.Contract.Features.RiskManagement.ScenarioHistories;
using SIMA.Domain.Models.Features.RiskManagement.ScenarioHistories.Contracts;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.RiskManagement.ScenarioHistories;

namespace SIMA.Application.Query.Features.RiskManagement.ScenarioHistories;

public class ScenarioHistoryQueryHandler: IQueryHandler<GetAllScenarioHistoryQuery, Result<IEnumerable<GetScenarioHistoryQueryResult>>>,
    IQueryHandler<GetScenarioHistoryQuery, Result<GetScenarioHistoryQueryResult>>
{
    private readonly IScenarioHistoryQureyRepository _repository;

    public ScenarioHistoryQueryHandler(IScenarioHistoryQureyRepository repository)
    {
        _repository = repository;
    }


    public async Task<Result<IEnumerable<GetScenarioHistoryQueryResult>>> Handle(GetAllScenarioHistoryQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }

    public async Task<Result<GetScenarioHistoryQueryResult>> Handle(GetScenarioHistoryQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request.Id);
        return Result.Ok(result);
    }
}