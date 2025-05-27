using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.OwnershipTypes;

public class GetAllOwnershipTypesQuery : BaseRequest, IQuery<Result<IEnumerable<GetOwnershipTypeQueryResult>>>
{
}
