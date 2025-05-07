using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.CurrentOccurrenceProbabilities;

public class ModifyCurrentOccurrenceProbabilityCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public string Code { get; set; }
    public long CurrentOccurrenceProbabilityValueId { get; set; }
    public long InherentOccurrenceProbabilityValueId { get; set; }
    public long FrequencyId { get; set; }
}