using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.InherentOccurrenceProbabilities;

public class DeleteInherentOccurrenceProbabilityCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}