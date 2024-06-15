using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Logistics.GoodsCategories;

public class ModifyGoodsCategoryCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long GoodsTypeId { get; set; }
    public string? IsTechnological { get; set; }
    public string? IsHardware { get; set; }
    public string? IsGoods { get; set; }
    public string? IsRequiredSecurityCheck { get; set; }
}