using SIMA.Domain.Models.Features.ServiceCatalogs.Channels.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.Products.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.Channels.Entities;

public class ProductChannel:Entity
{
    public ProductChannel()
    {
    }
    public ProductChannel(CreateProductChannelArg arg)
    {
        Id = new ProductChannelId(arg.Id);
        ProductId = new ProductId(arg.ProductId);
        ChannelId = new ChannelId(arg.ChannelId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static ProductChannel Create(CreateProductChannelArg arg)
    {
        return new ProductChannel(arg);
    }

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

    public void ChangeStatus(ActiveStatusEnum status)
    {
        ActiveStatusId = (long)status;
    }

    public ProductChannelId Id { get; set; }
    public virtual Product Product { get; private set; }
    public ProductId ProductId { get; private set; }
    public virtual Channel Channel { get; private set; }
    public ChannelId ChannelId { get; private set; }
    public long ActiveStatusId { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
