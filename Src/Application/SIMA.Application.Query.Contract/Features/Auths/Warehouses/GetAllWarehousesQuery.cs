using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Warehouses;

public class GetAllWarehousesQuery : BaseRequest, IQuery<Result<IEnumerable<GetWarehouseQueryResult>>>
{
}