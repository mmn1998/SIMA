using SIMA.Application.Query.Contract.Features.RiskManagement.Risks;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.RiskManagement.Risks;

namespace SIMA.Application.Query.Features.RiskManagement.Risks;

public class RiskQueryHandler : IQueryHandler<GetAllRisksQuery, Result<IEnumerable<GetAllRisksQueryResult>>>,
    IQueryHandler<GetRiskQuery, Result<GetRiskQueryResult>>

{
    private readonly IRiskQueryRepository _repository;

    public RiskQueryHandler(IRiskQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetRiskQueryResult>> Handle(GetRiskQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request.Id);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetAllRisksQueryResult>>> Handle(GetAllRisksQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}
