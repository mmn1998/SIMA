using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.CurrentOccurrenceProbabilities;

public class CreateCurrentOccurrenceProbabilityCommand : ICommand<Result<long>>
{
    public string Code { get; set; }
    public long CurrentOccurrenceProbabilityValueId { get; set; }
    public long InherentOccurrenceProbabilityValueId { get; set; }
    public long FrequencyId { get; set; }
}