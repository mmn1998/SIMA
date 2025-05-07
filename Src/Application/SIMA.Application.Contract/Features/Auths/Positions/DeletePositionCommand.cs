using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.Positions;

public class DeletePositionCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}
