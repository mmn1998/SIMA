using SIMA.Domain.Models.Features.Auths.Suppliers.Entities;
using SIMA.Domain.Models.Features.Auths.Suppliers.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.GoodsCategories.Args;
using SIMA.Domain.Models.Features.Logistics.GoodsCategories.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.Logistics.GoodsCategories.Entities;

public class GoodsCategorySupplier : Entity
{
    private GoodsCategorySupplier() { }
    private GoodsCategorySupplier(CreateGoodsCategorySupplierArg arg)
    {
        Id = new(arg.Id);
        SupplierId = new(arg.SupplierId);
        GoodsCategoryId = new(arg.GoodsCategoryId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static GoodsCategorySupplier Create(CreateGoodsCategorySupplierArg arg)
    {
        return new GoodsCategorySupplier(arg);
    }
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
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    public void Active(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Active;
    }
}
