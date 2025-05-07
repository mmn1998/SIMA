using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.CobitScenarios;

public class DeleteCobitScenarioCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}