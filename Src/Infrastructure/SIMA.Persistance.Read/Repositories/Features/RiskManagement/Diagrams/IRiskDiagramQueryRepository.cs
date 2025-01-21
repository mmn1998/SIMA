using SIMA.Application.Query.Contract.Features.RiskManagement.Diagrams;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.Diagrams;

public interface IRiskDiagramQueryRepository : IQueryRepository
{
    Task<GetrRskEvaluationQueryResult> GetRiskEvaluation(GetrRskEvaluationQuery query);
}