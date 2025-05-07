using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.Users;

public class UpdateUserLocationCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public long ActiveStatusId { get; set; }

    public long UserId { get; set; }
    public long? LocationId { get; set; }
}
