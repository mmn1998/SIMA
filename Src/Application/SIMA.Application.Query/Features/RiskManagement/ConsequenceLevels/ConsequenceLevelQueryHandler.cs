using SIMA.Application.Query.Contract.Features.RiskManagement.ConsequenceLevels;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.RiskManagement.ConsequenceLevels;

namespace SIMA.Application.Query.Features.RiskManagement.ConsequenceLevels;

public class ConsequenceLevelQueryHandler : IQueryHandler<GetConsequenceLevelQuery, Result<GetConsequenceLevelQueryResult>>,
    IQueryHandler<GetAllConsequenceLevelsQuery, Result<IEnumerable<GetConsequenceLevelQueryResult>>>
{
    private readonly IConsequenceLevelQueryRepository _repository;

    public ConsequenceLevelQueryHandler(IConsequenceLevelQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetConsequenceLevelQueryResult>> Handle(GetConsequenceLevelQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetConsequenceLevelQueryResult>>> Handle(GetAllConsequenceLevelsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}