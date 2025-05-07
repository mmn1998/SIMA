using SIMA.Application.Query.Contract.Features.RiskManagement.RiskValues;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.RiskManagement.RiskValues;

namespace SIMA.Application.Query.Features.RiskManagement.RiskValues;

public class RiskValueQueryHandler : IQueryHandler<GetRiskValueQuery, Result<GetRiskValueQueryResult>>,
    IQueryHandler<GetAllRiskValuesQuery, Result<IEnumerable<GetRiskValueQueryResult>>>
{
    private readonly IRiskValueQueryRepository _repository;

    public RiskValueQueryHandler(IRiskValueQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetRiskValueQueryResult>> Handle(GetRiskValueQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetRiskValueQueryResult>>> Handle(GetAllRiskValuesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}