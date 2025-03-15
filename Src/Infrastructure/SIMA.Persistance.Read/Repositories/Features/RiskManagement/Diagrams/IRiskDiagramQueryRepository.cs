using SIMA.Application.Query.Contract.Features.RiskManagement.Diagrams;
using SIMA.Application.Query.Contract.Features.RiskManagement.Risks;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.Diagrams;

public interface IRiskDiagramQueryRepository : IQueryRepository
{
    Task<GetrRskEvaluationQueryResult> GetRiskEvaluation(GetrRskEvaluationQuery query);
    Task<GetMatrixAChartQueryResult> GetMatrixAChart(GetMatrixAChartQuery query);
    Task<GetSeverityQueryResult> GetSeverity(GetSeverityQuery query);
    Task<GetInherentOccurrenceProbabilityQueryResult> GetInherentOccurrenceProbability(GetInherentOccurrenceProbabilityQuery query);
    Task<GetRiskLevelCobitQueryResult> GetRiskLevelCobit(GetRiskLevelCobitQuery query);
    Task<GetCurrentOccurrenceProbabilityQueryResult> GetCurrentOccurrenceProbability(GetCurrentOccurrenceProbabilityQuery query);
}