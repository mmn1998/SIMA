using SIMA.Domain.Models.Features.Auths.Domains.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Groups.Entities;
using SIMA.Domain.Models.Features.Auths.Permissions.Args;
using SIMA.Domain.Models.Features.Auths.Permissions.Interfaces;
using SIMA.Domain.Models.Features.Auths.Permissions.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Roles.Entities;
using SIMA.Domain.Models.Features.Auths.Users.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;

namespace SIMA.Domain.Models.Features.Auths.Permissions.Entities;

public class Permission : Entity
{
    private Permission()
    {

    }
    private Permission(CreatePermissionArg arg)
    {
        Id = new PermissionId(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
        EnglishKey = arg.EnglishKey;
        ActiveStatusId = arg.ActiveStatusId;
        DomainId = new DomainId(arg.DomainId);
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public static async Task<Permission> Create(CreatePermissionArg arg, IPermissionService service)
    {
        await CreateGuards(arg, service);
        return new Permission(arg);
    }

    private static async Task CreateGuards(CreatePermissionArg arg, IPermissionService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.EnglishKey.NullCheck();
        arg.ActiveStatusId.NullCheck();

        if (!await service.IsCodeUnique(arg.Code, 0)) throw new SimaResultException(CodeMessges._400Code,Messages.UniqueCodeError);
    }

    public PermissionId Id { get; private set; }

    public DomainId DomainId { get; private set; }

    public string? Name { get; private set; }

    public string? EnglishKey { get; private set; }

    public string? Code { get; private set; }

    public long ActiveStatusId { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }

    public virtual Domains.Entities.Domain Domain { get; private set; } = null!;

    private List<GroupPermission> _groupPermision = new();
    public ICollection<GroupPermission> GroupPermissions => _groupPermision;

    private List<RolePermission> _rolePermision = new();
    public ICollection<RolePermission> RolePermissions => _rolePermision;

    private List<UserPermission> _userPermision = new();
    public ICollection<UserPermission> UserPermissions => _userPermision;
    public void Delete()
    {
        ActiveStatusId = (int)ActiveStatusEnum.Delete;
    }
}
