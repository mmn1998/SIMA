using SIMA.Domain.Models.Features.Auths.Roles.Entities;
using SIMA.Domain.Models.Features.Auths.Roles.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.Args;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Framework.Common.Helper;

namespace SIMA.Domain.Models.Features.Auths.Users.Entities;

public class UserRole
{
    private UserRole()
    {

    }
    private UserRole(CreateUserRoleArg arg)
    {
        Id = new UserRoleId(IdHelper.GenerateUniqueId());
        UserId = new UserId(arg.UserId);
        RoleId = new RoleId(arg.RoleId);
        CreatedBy = arg.CreatedBy;
        ActiveStatusId = arg.ActiveStatusId;
        ActiveFrom = DateOnly.FromDateTime(DateTime.Now);
        CreatedAt = DateTime.Now;
        CreatedBy = arg.CreatedBy;
    }
    public void Modify(ModifyUserRoleArg arg)
    {
        UserId = new UserId(arg.UserId);
        RoleId = new RoleId(arg.RoleId);
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }
    public static async Task<UserRole> Create(CreateUserRoleArg arg)
    {
        return new UserRole(arg);
    }
    public UserRoleId Id { get; private set; }

    public UserId? UserId { get; private set; }

    public RoleId RoleId { get; private set; }

    public long ActiveStatusId { get; private set; }

    public DateOnly? ActiveFrom { get; private set; }

    public DateOnly? ActiveTo { get; private set; }

    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }

    public virtual Role? Role { get; private set; }

    public virtual User? User { get; private set; }
    public void Delete()
    {
        ActiveStatusId = (int)ActiveStatusEnum.Delete;
    }
}
