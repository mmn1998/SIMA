using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.BCP.HappeningPossiblities;

public class DeleteHappeningPossibilityCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}