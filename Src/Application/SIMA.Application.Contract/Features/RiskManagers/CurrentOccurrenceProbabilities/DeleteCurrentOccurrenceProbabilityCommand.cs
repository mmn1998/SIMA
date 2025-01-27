using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.CurrentOccurrenceProbabilities;

public class DeleteCurrentOccurrenceProbabilityCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}