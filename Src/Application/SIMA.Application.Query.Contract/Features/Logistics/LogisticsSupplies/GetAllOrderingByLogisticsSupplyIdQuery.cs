using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Logistics.LogisticsSupplies;

public class GetAllOrderingByLogisticsSupplyIdQuery : IQuery<Result<IEnumerable<GetOrderingByLogisticsSupplyIdQueryResult>>>
{
    public long LogisticsSupplyId { get; set; }
}
public class GetAllPaymentCommandByLogisticsSupplyIdQuery : IQuery<Result<IEnumerable<GetOrderingByLogisticsSupplyIdQueryResult>>>
{
    public long LogisticsSupplyId { get; set; }
}
public class GetOrderingByLogisticsSupplyIdQueryResult
{
    /// <summary>
    /// OrderingId
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// ReceiptNumber
    /// </summary>
    public string? Name { get; set; }
}
