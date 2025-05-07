using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.InherentOccurrenceProbabilities;

public class CreateInherentOccurrenceProbabilityCommand : ICommand<Result<long>>
{
    public string Code { get; set; }
    public long MatrixAValueId { get; set; }
    public long InherentOccurrenceProbabilityValueId { get; set; }
    public long ScenarioHistoryId { get; set; }
}