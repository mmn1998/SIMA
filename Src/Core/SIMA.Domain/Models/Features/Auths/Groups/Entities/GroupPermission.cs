using SIMA.Domain.Models.Features.Auths.Groups.Args;
using SIMA.Domain.Models.Features.Auths.Groups.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Permissions.Entities;
using SIMA.Domain.Models.Features.Auths.Permissions.ValueObjects;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Domain.Models.Features.Auths.Groups.Entities;

public class GroupPermission
{
    private GroupPermission()
    {

    }
    private GroupPermission(CreateGroupPermissionArg arg)
    {
        Id = new GroupPermissionId(IdHelper.GenerateUniqueId());
        GroupId = new GroupId(arg.GroupId);
        PermissionId = new PermissionId(arg.PermissionId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<GroupPermission> Create(CreateGroupPermissionArg arg)
    {
        return new GroupPermission(arg);
    }
    public async Task Modify(ModifyGroupPermissionArg arg)
    {
        PermissionId = new PermissionId(arg.PermissionId);
        GroupId = new GroupId(arg.GroupId);
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
        ActiveStatusId = arg.ActiveStatusId;
    }

    public async Task ChangeStatus(ActiveStatusEnum status)
    {
        ActiveStatusId = (long)status;
    }

    public GroupPermissionId Id { get; private set; }

    public GroupId? GroupId { get; private set; }

    public PermissionId PermissionId { get; private set; }

    public long ActiveStatusId { get; private set; }

    public DateOnly? ActiveFrom { get; private set; }

    public DateOnly? ActiveTo { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }

    public virtual Group? Group { get; private set; }

    public virtual Permission? Permission { get; private set; }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
