using SIMA.Domain.Models.Features.Auths.ResponsibleTypes;
using SIMA.Domain.Models.Features.Auths.Roles.ValueObjects;
using SIMA.Domain.Models.Features.Auths.ActiveStatuses.Entities;
using SIMA.Domain.Models.Features.Auths.ResponsibleTypes.Args;
using SIMA.Domain.Models.Features.Auths.ResponsibleTypes.Entities;
using SIMA.Domain.Models.Features.Auths.ResponsibleTypes.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.Channels.Args;
using SIMA.Framework.Common.Helper;
using System.Text;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.Channels.Entities;

public class ChannelResponsible :Entity
{

    public ChannelResponsible()
    {

    }

    public ChannelResponsible(CreateChannelResponsibleArg arg)
    {
        Id = new ChannelResponsibleId(arg.Id);
        ChannelId = new ChannelId(arg.ChannelId);
        ResponsibleId = new StaffId(arg.ResponsibleId);
        ResponsibleTypeId = new ResponsibleTypeId(arg.ResponsibleTypeId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;  
    }

    public static ChannelResponsible Create(CreateChannelResponsibleArg arg)
    {
        return new ChannelResponsible(arg);
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

    public ChannelResponsibleId Id { get; private set; }
    public virtual Channel Channel { get; private set; } 
    public ChannelId ChannelId { get; private set; }
    public virtual ResponsibleType? ResponsibleType { get; private set; }
    public ResponsibleTypeId? ResponsibleTypeId { get; private set; }
    public virtual Staff Responsible { get; private set; }
    public StaffId ResponsibleId { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public long CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

}
