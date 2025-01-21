using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.RiskManagers.EvaluationCriterias;

public class ModifyEvaluationCriteriaCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public string? Code { get; set; }
    public long RiskDegreeId { get; set; }
    public long RiskPossibilityId { get; set; }
    public long RiskImpactId { get; set; }
}