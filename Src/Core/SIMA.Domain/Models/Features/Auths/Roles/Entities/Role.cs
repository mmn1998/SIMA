using SIMA.Domain.Models.Features.Auths.Forms.Entities;
using SIMA.Domain.Models.Features.Auths.Permissions.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Roles.Args;
using SIMA.Domain.Models.Features.Auths.Roles.Exceptions;
using SIMA.Domain.Models.Features.Auths.Roles.Interfaces;
using SIMA.Domain.Models.Features.Auths.Roles.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Entites;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.Auths.Roles.Entities;

public class Role : Entity
{
    private Role()
    {

    }
    private Role(CreateRoleArg arg)
    {
        Id = new RoleId(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        EnglishKey = arg.EnglishKey;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        Code = arg.Code;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<Role> Create(IRoleService roleService, CreateRoleArg arg)
    {
        await CreateGuards(arg, roleService);
        return new Role(arg);
    }
    #region Guards
    private static async Task CreateGuards(CreateRoleArg arg, IRoleService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.EnglishKey.NullCheck();
        arg.ActiveStatusId.NullCheck();

        if (!await service.IsRoleSatisfied(arg.Code, arg.EnglishKey, 0)) throw RoleExceptions.RoleNotSatisfiedException;
    }
    private async Task ModifyGuards(ModifyRoleArg arg, IRoleService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.EnglishKey.NullCheck();
        arg.ActiveStatusId.NullCheck();

        if (await service.IsRoleSatisfied(arg.Code, arg.EnglishKey, arg.Id)) throw RoleExceptions.RoleNotSatisfiedException;
    }
    #endregion
    public async Task AddRolePermission(CreateRolePermissionArg arg)
    {
        if (_rolePermissions.Any(rp => rp.PermissionId == new PermissionId(arg.PermissionId) && rp.RoleId == new RoleId(arg.RoleId)))
        {
            throw SimaResultException.RolePermoissionDuplicateError;
        }
        var entity = await RolePermission.Create(arg);
        _rolePermissions.Add(entity);
    }
    public async Task AddRolePermissions(List<CreateRolePermissionArg> args)
    {
        foreach (var arg in args)
        {
            await AddRolePermission(arg);
        }
    }
    public async Task ModifyRolePermission(ModifyRolePermissionArg arg)
    {
        if (_rolePermissions.Any(rp => rp.PermissionId == new PermissionId(arg.PermissionId) && rp.RoleId == new RoleId(arg.RoleId)))
        {
            throw SimaResultException.RolePermoissionDuplicateError;
        }
        var rolePermission = _rolePermissions.FirstOrDefault(rp => rp.Id == new RolePermissionId(arg.Id));
        rolePermission.NullCheck();
        rolePermission.Modify(arg);
    }
    public async Task Modify(ModifyRoleArg arg, IRoleService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        EnglishKey = arg.EnglishKey;
        Code = arg.Code;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public void DeleteRolePermission(long rolePermissionId)
    {
        var entity = _rolePermissions.FirstOrDefault(rp => rp.Id == new RolePermissionId(rolePermissionId));
        entity.NullCheck();
        entity.Delete();
    }


    public RoleId Id { get; private set; }

    public string? Name { get; private set; }

    public string? EnglishKey { get; private set; }

    public string? Code { get; private set; }

    public long ActiveStatusId { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }
    private List<RolePermission> _rolePermissions = new();
    public ICollection<RolePermission> RolePermissions => _rolePermissions;
    private List<UserRole> _userRoles = new();
    public ICollection<UserRole> UserRoles => _userRoles;

    private List<FormRole> _formRoles = new();
    public ICollection<FormRole> FormRoles => _formRoles;
    private List<WorkFlow> _workFlows = new();
    public ICollection<WorkFlow> WorkFlows => _workFlows;
    private List<WorkFlowActorRole> _workFlowActorRoles = new();
    public ICollection<WorkFlowActorRole> WorkFlowActorRoles => _workFlowActorRoles;

    public void Delete()
    {
        ActiveStatusId = (int)ActiveStatusEnum.Delete;
    }
}
