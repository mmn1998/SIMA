using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.ScenarioHistories;

public class CreateScenarioHistoryCommand: ICommand<Result<long>>
{
    public string Name { get; set; }
    public string Code { get; set; }
    public float NumericValue { get; set; }
}