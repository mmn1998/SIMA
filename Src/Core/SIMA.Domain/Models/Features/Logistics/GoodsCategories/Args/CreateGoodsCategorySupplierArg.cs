namespace SIMA.Domain.Models.Features.Logistics.GoodsCategories.Args;

public class CreateGoodsCategorySupplierArg
{
    public long Id { get; set; }
    public long GoodsCategoryId { get; set; }
    public long SupplierId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}
