using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.CurrentOccurrenceProbabilityValues;

public class DeleteCurrentOccurrenceProbabilityValueCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}