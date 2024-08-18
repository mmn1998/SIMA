
using SIMA.Domain.Models.Features.Auths.UserTypes.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Channels.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.Channels.Entities;

public class ChannelUserType
{

    public ChannelUserType()
    {

    }

    public ChannelUserType(CreateChannelUserTypeArg arg)
    {
        Id = new ChannelUserTypeId(arg.Id);
        ChannelId = new ChannelId(arg.ChannelId);
        UserTypeId = new UserTypeId(arg.UserTypeId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static ChannelUserType Create(CreateChannelUserTypeArg arg)
    {
        return new ChannelUserType(arg);
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

    public ChannelUserTypeId Id { get; private set; }
    public virtual Channel Channel { get; private set; } 
    public ChannelId ChannelId { get; private set; }
    public virtual UserType UserType { get; private set; }
    public UserTypeId UserTypeId { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public long CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}
