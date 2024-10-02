using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Logistics.Goods;

public class CreateGoodsCommand : ICommand<Result<long>>
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long GoodsCategoryId { get; set; }public long UnitMeasurementId { get; set; }
    public string? IsFixedAsset { get; set; }
}