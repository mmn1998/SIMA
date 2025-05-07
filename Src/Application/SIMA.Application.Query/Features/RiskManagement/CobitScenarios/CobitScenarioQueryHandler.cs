using SIMA.Application.Query.Contract.Features.RiskManagement.CobitScenarios;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.RiskManagement.CobitScenarios;

namespace SIMA.Application.Query.Features.RiskManagement.CobitScenarios;

public class CobitScenarioQueryHandler : IQueryHandler<GetCobitScenarioQuery, Result<GetCobitScenarioQueryResult>>,
    IQueryHandler<GetAllCobitScenariosQuery, Result<IEnumerable<GetCobitScenarioQueryResult>>>,
    IQueryHandler<GetCobitScenariosByCategoryQuery, Result<IEnumerable<GetCobitScenarioQueryResult>>>
{
    private readonly ICobitScenarioQueryRepository _repository;

    public CobitScenarioQueryHandler(ICobitScenarioQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetCobitScenarioQueryResult>> Handle(GetCobitScenarioQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetCobitScenarioQueryResult>>> Handle(GetAllCobitScenariosQuery request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetAll(request);
        return data;
    }

    public async Task<Result<IEnumerable<GetCobitScenarioQueryResult>>> Handle(GetCobitScenariosByCategoryQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetAllByCategory(request);
        return Result.Ok(result);
    }
}