
using SIMA.Domain.Models.Features.ServiceCatalogs.Channels.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.Channels.Entities;

public class ChannelAccessPoint
{
    public ChannelAccessPoint()
    {

    }

    public ChannelAccessPoint(CreateChannelAccessPointArg arg)
    {
        Id = new ChannelAccessPointId(arg.Id);
        ChannelId =new ChannelId(arg.ChannelId);
        IpAddress = arg.IpAddress;
        Port = arg.Port;
        ActivationDate = arg.ActivationDate;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static ChannelAccessPoint Create(CreateChannelAccessPointArg arg)
    {
        return new ChannelAccessPoint(arg);
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

    public ChannelAccessPointId Id { get; private set; }
    public virtual Channel Channel { get; private set; }
    public ChannelId ChannelId { get; private set; }
    public string? IpAddress { get; private set; }
    public string? Port { get; private set; }
    public DateTime? ActivationDate { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public long CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}
