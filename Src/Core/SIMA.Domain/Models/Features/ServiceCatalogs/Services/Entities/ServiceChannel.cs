using SIMA.Domain.Models.Features.ChannelTypeCatalogs.ChannelTypes.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Args;
using SIMA.Framework.Core.Entities;

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
        ChannelTypeId = new ChannelTypeId(arg.ChannelTypeId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<ServiceChannel> Create(CreateServiceChannelArg arg)
    {
        return new ServiceChannel(arg);
    }
    public ServiceChannelId Id { get; private set; }
    public virtual Service Service { get; private set; }
    public ServiceId ServiceId { get; private set; }
    public virtual ChannelType ChannelType { get; private set; }
    public ChannelTypeId ChannelTypeId { get; private set; }
    public long? ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}
