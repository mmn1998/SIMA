using SIMA.Domain.Models.Features.Auths.Groups.Args;
using SIMA.Domain.Models.Features.Auths.Groups.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.Entities;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Domain.Models.Features.Auths.Groups.Entities;

public class UserGroup
{
    private UserGroup()
    {

    }
    private UserGroup(CreateUserGroupArg arg)
    {
        Id = new UserGroupId(IdHelper.GenerateUniqueId());
        if (arg.UserId.HasValue) UserId = new UserId(arg.UserId.Value);
        if (arg.GroupId.HasValue) GroupId = new GroupId(arg.GroupId.Value);
        ActiveStatusId = arg.ActiveStatusId;
        ActiveFrom = arg.ActiveFrom;
        ActiveTo = arg.ActiveTo;
        CreatedAt = DateTime.Now; ;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<UserGroup> Create(CreateUserGroupArg arg)
    {
        return new UserGroup(arg);
    }
    public async Task Modify(ModifyUserGroupArg arg)
    {
        if (arg.UserId.HasValue) UserId = new UserId(arg.UserId.Value);
        GroupId = new GroupId(arg.GroupId);
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }

    public async Task ChangeStatus(ActiveStatusEnum status)
    {
        ActiveStatusId = (long)status;
    }
    public UserGroupId Id { get; private set; }

    public UserId? UserId { get; private set; }

    public GroupId GroupId { get; private set; }

    public long ActiveStatusId { get; private set; }

    public DateOnly? ActiveFrom { get; private set; }

    public DateOnly? ActiveTo { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }

    public virtual Group? Group { get; private set; }

    public virtual User? User { get; private set; }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
