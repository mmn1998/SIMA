using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.OwnershipTypes;

public class DeleteOwnershipTypeCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}