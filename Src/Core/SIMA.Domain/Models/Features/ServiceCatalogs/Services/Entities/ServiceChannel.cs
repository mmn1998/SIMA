using SIMA.Domain.Models.Features.ServiceCatalogs.Channels.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;

public class ServiceChannel : Entity
{
    private ServiceChannel()
    {
    }
    private ServiceChannel(CreateServiceChannelArg arg)
    {
        Id = new ServiceChannelId(arg.Id);
        ServiceId = new ServiceId(arg.ServiceId);
        ChannelId = new(arg.ChannelId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static ServiceChannel Create(CreateServiceChannelArg arg)
    {
        return new ServiceChannel(arg);
    }
    public ServiceChannelId Id { get; private set; }
    public virtual Service Service { get; private set; }
    public ServiceId ServiceId { get; private set; }
    public virtual Channel Channel { get; private set; }
    public ChannelId ChannelId { get; private set; }
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
