using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.BCP.BusinessContinuityStrategies;

public class DeleteBusinessContinuityStrategyCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}
