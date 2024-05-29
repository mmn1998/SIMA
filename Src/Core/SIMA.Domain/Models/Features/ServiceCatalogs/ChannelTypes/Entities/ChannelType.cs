using SIMA.Domain.Models.Features.ServiceCatalogs.ChannelTypes.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.ChannelTypeCatalogs.ChannelTypes.Entities;

public class ChannelType : Entity
{
    private ChannelType()
    {
    }
    private ChannelType(CreateChannelTypeArg arg)
    {
        Id = new ChannelTypeId(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<ChannelType> Create(CreateChannelTypeArg arg)
    {
        return new ChannelType(arg);
    }
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    public ChannelTypeId Id { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public long? ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    private List<ServiceChannel> _serviceChanneles = new();
    public ICollection<ServiceChannel> ServiceChanneles => _serviceChanneles;
}
