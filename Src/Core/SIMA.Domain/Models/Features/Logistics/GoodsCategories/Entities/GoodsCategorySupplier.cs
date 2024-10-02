using SIMA.Domain.Models.Features.Auths.Suppliers.Entities;
using SIMA.Domain.Models.Features.Auths.Suppliers.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.GoodsCategories.ValueObjects;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.Logistics.GoodsCategories.Entities;

public class GoodsCategorySupplier : Entity
{
    private GoodsCategorySupplier() { }
    public GoodsCategorySupplierId Id { get; private set; }
    public SupplierId SupplierId { get; private set; }
    public virtual Supplier Supplier { get; private set; }
    public GoodsCategoryId GoodsCategoryId { get; private set; }
    public virtual GoodsCategory GoodsCategory { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public long CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}
