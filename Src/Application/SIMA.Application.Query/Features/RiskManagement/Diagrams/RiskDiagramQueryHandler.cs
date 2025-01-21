using SIMA.Application.Query.Contract.Features.RiskManagement.Diagrams;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.RiskManagement.Diagrams;

namespace SIMA.Application.Query.Features.RiskManagement.Diagrams;

public class RiskDiagramQueryHandler : IQueryHandler<GetrRskEvaluationQuery, Result<GetrRskEvaluationQueryResult>>
{
    private readonly IRiskDiagramQueryRepository _repository;

    public RiskDiagramQueryHandler(IRiskDiagramQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetrRskEvaluationQueryResult>> Handle(GetrRskEvaluationQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetRiskEvaluation(request);
    }
}
