using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.BCP.Origins;

public class DeleteOriginCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}