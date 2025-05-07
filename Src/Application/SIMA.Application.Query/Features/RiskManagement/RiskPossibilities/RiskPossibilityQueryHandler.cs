using SIMA.Application.Query.Contract.Features.RiskManagement.RiskPossibilities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.RiskManagement.RiskPossibilities;

namespace SIMA.Application.Query.Features.RiskManagement.RiskPossibilities
{
    public class RiskPossibilityQueryHandler :
    IQueryHandler<GetAllRiskPossibilitiesQuery, Result<IEnumerable<GetRiskPossibilitiesQueryResult>>>,
    IQueryHandler<GetRiskPossibilityQuery, Result<GetRiskPossibilitiesQueryResult>>
    {
        private readonly IRiskPossibilityQueryRepository _repository;

    public RiskPossibilityQueryHandler(IRiskPossibilityQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<IEnumerable<GetRiskPossibilitiesQueryResult>>> Handle(GetAllRiskPossibilitiesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }

    public async Task<Result<GetRiskPossibilitiesQueryResult>> Handle(GetRiskPossibilityQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request.Id);
        return Result.Ok(result);
    }
}
}
