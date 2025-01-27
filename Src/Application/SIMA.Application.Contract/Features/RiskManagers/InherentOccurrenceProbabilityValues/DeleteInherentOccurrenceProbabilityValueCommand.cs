using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.InherentOccurrenceProbabilityValues;

public class DeleteInherentOccurrenceProbabilityValueCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}