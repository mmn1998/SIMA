using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Logistics.LogisticsSupplies;

public class GetAllLogisticsSuppliesQuery : BaseRequest, IQuery<Result<IEnumerable<GetLogisticsSupplyQueryResult>>>
{
}
public class GetAllMyLogisticsSuppliesQuery : BaseRequest, IQuery<Result<IEnumerable<GetLogisticsSupplyQueryResult>>>
{
}