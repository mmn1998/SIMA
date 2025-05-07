using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.BCP.RecoveryPointObjectives;

public class DeleteRecoveryPointObjectiveCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}