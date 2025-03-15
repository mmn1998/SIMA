using SIMA.Application.Query.Contract.Features.RiskManagement.Diagrams;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.RiskManagement.Diagrams;

namespace SIMA.Application.Query.Features.RiskManagement.Diagrams;

public class RiskDiagramQueryHandler : IQueryHandler<GetrRskEvaluationQuery, Result<GetrRskEvaluationQueryResult>>,IQueryHandler<GetMatrixAChartQuery, Result<GetMatrixAChartQueryResult>>,IQueryHandler<GetInherentOccurrenceProbabilityQuery, Result<GetInherentOccurrenceProbabilityQueryResult>>,IQueryHandler<GetRiskLevelCobitQuery, Result<GetRiskLevelCobitQueryResult>>,IQueryHandler<GetSeverityQuery, Result<GetSeverityQueryResult>>,IQueryHandler<GetCurrentOccurrenceProbabilityQuery, Result<GetCurrentOccurrenceProbabilityQueryResult>>
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

    public async Task<Result<GetMatrixAChartQueryResult>> Handle(GetMatrixAChartQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetMatrixAChart(request);
    }

    public async Task<Result<GetInherentOccurrenceProbabilityQueryResult>> Handle(GetInherentOccurrenceProbabilityQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetInherentOccurrenceProbability(request);
    }

    public async Task<Result<GetRiskLevelCobitQueryResult>> Handle(GetRiskLevelCobitQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetRiskLevelCobit(request);
    }

    public async Task<Result<GetSeverityQueryResult>> Handle(GetSeverityQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetSeverity(request);
    }

    public async Task<Result<GetCurrentOccurrenceProbabilityQueryResult>> Handle(GetCurrentOccurrenceProbabilityQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetCurrentOccurrenceProbability(request);
    }
}
