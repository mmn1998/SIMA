using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.OwnershipTypes;

public class GetOwnershipTypeQuery : IQuery<Result<GetOwnershipTypeQueryResult>>
{
    public long Id { get; set; }
}

