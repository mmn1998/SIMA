using SIMA.Application.Query.Contract.Features.RiskManagement.RiskLevelCobits;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.RiskManagement.RiskLevelCobits;

namespace SIMA.Application.Query.Features.RiskManagement.RiskLevelCobits;

public class RiskLevelCobitQueryHandler : IQueryHandler<GetRiskLevelCobitQuery, Result<GetRiskLevelCobitQueryResult>>,
    IQueryHandler<GetAllRiskLevelCobitsQuery, Result<IEnumerable<GetRiskLevelCobitQueryResult>>>
{
    private readonly IRiskLevelCobitQueryRepository _repository;

    public RiskLevelCobitQueryHandler(IRiskLevelCobitQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetRiskLevelCobitQueryResult>> Handle(GetRiskLevelCobitQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetRiskLevelCobitQueryResult>>> Handle(GetAllRiskLevelCobitsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}