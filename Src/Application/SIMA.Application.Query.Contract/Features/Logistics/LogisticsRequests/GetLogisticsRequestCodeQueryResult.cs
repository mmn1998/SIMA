using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using System.ComponentModel;

namespace SIMA.Application.Query.Contract.Features.Logistics.LogisticsRequests;

public class GetLogisticsRequestCodeQuery : IQuery<Result<List<GetLogisticsRequestCodeQueryResult>>>
{
}
public class GetLogisticsRequestCodeQueryResult
{
    public long Id { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public string? NameAndCode { get; set; }
    public long GoodsStatusId { get; set; }
}





public class GetLogisticsRequestGoodsQuery : IQuery<Result<IEnumerable<GetLogisticsRequestGoodsQueryResult>>>
{
    public long LogisticsRequestId { get; set; }
}
public class GetLogisticsRequestGoodsQueryResult
{
    public long LogisticsRequestGoodsId { get; set; }
    public string? UnitMeasurementName { get; set; }
    public string? GoodsCategoryName { get; set; }
    public long? GoodsCategoryId { get; set; }
    public string? IssueCode { get; set; }
    public float? Quantity { get; set; }
}
