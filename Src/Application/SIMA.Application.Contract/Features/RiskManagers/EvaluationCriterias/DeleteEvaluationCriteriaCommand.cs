using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.RiskManagers.EvaluationCriterias;

public class DeleteEvaluationCriteriaCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}
