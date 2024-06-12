using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.RiskManagers.RiskCriterias;

public class DeleteRiskCriteriaCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}
