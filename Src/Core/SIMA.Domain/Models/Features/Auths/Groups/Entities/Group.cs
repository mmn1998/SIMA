using SIMA.Domain.Models.Features.Auths.Domains.Entities;
using SIMA.Domain.Models.Features.Auths.Groups.Args;
using SIMA.Domain.Models.Features.Auths.Groups.Interfaces;
using SIMA.Domain.Models.Features.Auths.Groups.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Permissions.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.Auths.Groups.Entities;

public class Group : Entity
{
    private Group()
    {

    }
    private Group(CreateGroupArg arg)
    {
        Id = new GroupId(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        ActiveStatusId = arg.ActiveStatusId;

    }
    public static async Task<Group> Create(CreateGroupArg arg, IGroupService service)
    {
        await CreateGuards(arg, service);
        return new Group(arg);
    }
    public async void Modify(ModifyGroupArg arg, IGroupService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    #region AddMethods

    public async Task AddGroupUser(CreateUserGroupArg arg)
    {
        arg.UserId.NullCheck();
        arg.GroupId.NullCheck();
        if (_userGroups.Any(ur => ur.GroupId == new GroupId(arg.GroupId.Value) && ur.UserId == new UserId(arg.UserId.Value)))
        {
            throw SimaResultException.UserGroupDuplicateError;
        }
        var entity = await UserGroup.Create(arg);
        _userGroups.Add(entity);
    }
    public async Task AddGroupPermission(CreateGroupPermissionArg arg)
    {
        if (_groupPermissions.Any(ur => ur.GroupId == new GroupId(arg.GroupId) && ur.PermissionId == new PermissionId(arg.PermissionId)))
        {
            throw SimaResultException.GroupPermoissionDuplicateError;
        }
        var entity = await GroupPermission.Create(arg);
        _groupPermissions.Add(entity);
    }
    public async Task AddGroupUsers(List<CreateUserGroupArg> args)
    {
        foreach (var arg in args)
        {
            await AddGroupUser(arg);
        }
    }
    public async Task AddGroupPermissions(List<CreateGroupPermissionArg> args)
    {
        foreach (var arg in args)
        {
            await AddGroupPermission(arg);
        }
    }
    #endregion
    #region ModifyMethods


    public void ModifyGroupUser(ModifyUserGroupArg arg)
    {
        arg.UserId.NullCheck();
        if (_userGroups.Any(ur => ur.GroupId == new GroupId(arg.GroupId) && ur.UserId == new UserId(arg.UserId.Value)))
        {
            throw SimaResultException.UserGroupDuplicateError;
        }
        var entity = _userGroups.FirstOrDefault(ug => ug.Id == new UserGroupId(arg.Id));
        entity.NullCheck();
        entity.Modify(arg);
    }
    public void ModifyGroupPermission(ModifyGroupPermissionArg arg)
    {
        var entity = _groupPermissions.FirstOrDefault(gp => gp.Id == new GroupPermissionId(arg.Id));
        entity.NullCheck();
        entity.Modify(arg);
    }
    #endregion
    #region DeleteMethods
    public void DeleteUserGroup(long userGroupId)
    {
        var entity = _userGroups.FirstOrDefault(g => g.Id == new UserGroupId(userGroupId));
        entity.NullCheck();
        entity.Delete();
    }
    public void DeleteGroupPermission(long groupPermissionId)
    {
        var entity = _groupPermissions.FirstOrDefault(gp => gp.Id == new GroupPermissionId(groupPermissionId));
        entity.NullCheck();
        entity.Delete();
    }
    #endregion
    #region Guards
    private static async Task CreateGuards(CreateGroupArg arg, IGroupService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ActiveStatusId.NullCheck();

        if (!await service.IsCodeUnique(arg.Code, 0)) throw SimaResultException.UniqueCodeError;
    }
    private async Task ModifyGuards(ModifyGroupArg arg, IGroupService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (!await service.IsCodeUnique(arg.Code, arg.Id)) throw SimaResultException.UniqueCodeError;
    }
    #endregion

    public GroupId Id { get; private set; }

    public string? Name { get; private set; }

    public string? Code { get; private set; }

    public long ActiveStatusId { get; private set; }

    public DateOnly? ActiveFrom { get; private set; }

    public DateOnly? ActiveTo { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }
    private List<GroupPermission> _groupPermissions = new();

    public ICollection<GroupPermission> GroupPermissions => _groupPermissions;

    private List<UserGroup> _userGroups = new();
    public ICollection<UserGroup> UserGroups => _userGroups;

    private List<FormGroup> _formGroups = new();
    public ICollection<FormGroup> FormGroups => _formGroups;


    public void Delete()
    {
        ActiveStatusId = (int)ActiveStatusEnum.Delete;
    }

}
