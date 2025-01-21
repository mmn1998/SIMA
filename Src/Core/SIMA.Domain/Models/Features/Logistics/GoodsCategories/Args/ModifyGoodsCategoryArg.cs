namespace SIMA.Domain.Models.Features.Logistics.GoodsCategories.Args;

public class ModifyGoodsCategoryArg
{
    public long GoodsTypeId { get; set; }
    public string IsTechnological { get; set; }
    public string IsHardware { get; set; }
    public string IsGoods { get; set; }
    public string IsRequiredSecurityCheck { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string IsFixedAsset { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}