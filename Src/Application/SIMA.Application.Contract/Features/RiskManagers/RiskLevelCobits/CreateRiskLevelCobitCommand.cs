using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.RiskLevelCobits;

public class CreateRiskLevelCobitCommand : ICommand<Result<long>>
{
    public string Code { get; set; }
    public float NumericValue { get; set; }
    public long SeverityId { get; set; }
    public long RiskLevelId { get; set; }
    public long CurrentOccurrenceProbabilityValueId { get; set; }
}