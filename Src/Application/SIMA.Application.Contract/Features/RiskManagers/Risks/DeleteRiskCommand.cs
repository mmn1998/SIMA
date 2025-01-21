using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.Risks;

public class DeleteRiskCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}
