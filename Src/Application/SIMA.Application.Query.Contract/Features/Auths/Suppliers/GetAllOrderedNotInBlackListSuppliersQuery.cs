using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Suppliers;

public class GetAllOrderedNotInBlackListSuppliersQuery : BaseRequest, IQuery<Result<IEnumerable<GetAllOrderedNotInBlackListSuppliersQueryResult>>>
{
}