using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.BCP.RecoveryPointObjectives;

public class CreateRecoveryPointObjectiveCommand : ICommand<Result<long>>
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public int RpoFrom { get; set; }
    public int RpoTo { get; set; }
}