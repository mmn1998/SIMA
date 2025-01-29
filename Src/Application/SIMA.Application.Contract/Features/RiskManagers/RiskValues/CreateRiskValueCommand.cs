using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.RiskValues;

public class CreateRiskValueCommand : ICommand<Result<long>>
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string Color { get; set; }
    public string Condition { get; set; }
    public float NumericValue { get; set; }
}