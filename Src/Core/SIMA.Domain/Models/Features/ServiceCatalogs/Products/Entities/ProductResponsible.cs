using SIMA.Domain.Models.Features.Auths.ResponsibleTypes.Args;
using SIMA.Domain.Models.Features.Auths.ResponsibleTypes.Entities;
using SIMA.Domain.Models.Features.Auths.ResponsibleTypes.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.Products.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.Products.Entities;

public class ProductResponsible : Entity
{
    private ProductResponsible()
    {
    }

    private ProductResponsible(CreateProductResponsibleArg arg)
    {
        Id = new ProductResponsibleId(arg.Id);
        ResponsilbeId = new StaffId(arg.ResponsilbeId);
        ResponsibleTypeId = new (arg.ResposibleTypeId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<ProductResponsible> Create(CreateProductResponsibleArg arg)
    {
        return new ProductResponsible(arg);
    }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    public ProductResponsibleId Id { get; private set; }
    public virtual Product Product { get; private set; }
    public ProductId ProductId { get; private set; }
    public virtual ResponsibleType? ResponsibleType { get; private set; }
    public ResponsibleTypeId? ResponsibleTypeId { get; private set; }
    public virtual Staff Responsilbe { get; private set; }
    public StaffId ResponsilbeId { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public long CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

}
