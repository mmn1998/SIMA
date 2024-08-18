using SIMA.Domain.Models.Features.Auths.Forms.Entities;
using SIMA.Domain.Models.Features.Auths.Groups.Args;
using SIMA.Domain.Models.Features.Auths.Groups.Interfaces;
using SIMA.Domain.Models.Features.Auths.Groups.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Permissions.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Entites;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Entites;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

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
    public async Task Modify(ModifyGroupArg arg, IGroupService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    #region AddMethods
    public async Task AddGroupPermission(List<CreateGroupPermissionArg> request, long groupId)
    {
        groupId.NullCheck();

        var previousGroups = _groupPermissions.Where(x => x.GroupId == new GroupId(groupId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

        var addPermission = request.Where(x => !previousGroups.Any(c => c.PermissionId.Value == x.PermissionId)).ToList();
        var deleteMember = previousGroups.Where(x => !request.Any(c => c.PermissionId == x.PermissionId.Value)).ToList();


        foreach (var permission in addPermission)
        {
            var entity = _groupPermissions.Where(x => (x.PermissionId == new PermissionId(permission.PermissionId) && x.GroupId == new GroupId(groupId)) && x.ActiveStatusId != (long)ActiveStatusEnum.Active).FirstOrDefault();
            if (entity is not null)
            {
                await entity.ChangeStatus(ActiveStatusEnum.Active);
            }
            else
            {
                entity = await GroupPermission.Create(permission);
                _groupPermissions.Add(entity);
            }
        }

        foreach (var permission in deleteMember)
        {
            permission.Delete((long)request[0].CreatedBy);
        }
    }
    public async Task AddUserGroup(List<CreateUserGroupArg> request, long groupId)
    {
        groupId.NullCheck();

        var previousGroups = _userGroups.Where(x => x.GroupId == new GroupId(groupId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

        var addGroup = request.Where(x => !previousGroups.Any(c => c.UserId.Value == x.UserId)).ToList();
        var deleteMember = previousGroups.Where(x => !request.Any(c => c.UserId == x.UserId.Value)).ToList();


        foreach (var group in addGroup)
        {
            var entity = _userGroups.Where(x => (x.GroupId == new GroupId(groupId) && x.UserId == new UserId((long)group.UserId)) && x.ActiveStatusId != (long)ActiveStatusEnum.Active).FirstOrDefault();
            if (entity is not null)
            {
                await entity.ChangeStatus(ActiveStatusEnum.Active);
            }
            else
            {
                entity = await UserGroup.Create(group);
                _userGroups.Add(entity);
            }
        }

        foreach (var group in deleteMember)
        {
            group.Delete((long)request[0].CreatedBy);
        }
    }

    #region old 
    //public async Task AddGroupUser(CreateUserGroupArg arg)
    //{
    //    arg.UserId.NullCheck();
    //    arg.GroupId.NullCheck();
    //    if (_userGroups.Any(ur => ur.GroupId == new GroupId(arg.GroupId.Value) && ur.UserId == new UserId(arg.UserId.Value)))
    //    {
    //        throw new SimaResultException(CodeMessges._100018Code, Messages.UserGroupDuplicateError);
    //    }
    //    var entity = await UserGroup.Create(arg);
    //    _userGroups.Add(entity);
    //}
    //public async Task AddGroupPermission(CreateGroupPermissionArg arg)
    //{
    //    if (_groupPermissions.Any(ur => ur.GroupId == new GroupId(arg.GroupId) && ur.PermissionId == new PermissionId(arg.PermissionId)))
    //    {
    //        throw new SimaResultException(CodeMessges._100018Code, Messages.GroupPermoissionDuplicateError);
    //    }
    //    var entity = await GroupPermission.Create(arg);
    //    _groupPermissions.Add(entity);
    //}
    //public async Task AddGroupUsers(List<CreateUserGroupArg> args)
    //{
    //    foreach (var arg in args)
    //    {
    //        await AddGroupUser(arg);
    //    }
    //}
    //public async Task AddGroupPermissions(List<CreateGroupPermissionArg> args)
    //{
    //    foreach (var arg in args)
    //    {
    //        await AddGroupPermission(arg);
    //    }
    //}
    #endregion

    #endregion

    #region ModifyMethods


    public async Task ModifyGroupUser(ModifyUserGroupArg arg)
    {
        arg.UserId.NullCheck();
        if (_userGroups.Any(ur => ur.GroupId == new GroupId(arg.GroupId) && ur.UserId == new UserId(arg.UserId.Value)))
        {
            throw new SimaResultException(CodeMessges._100018Code, Messages.UserGroupDuplicateError);
        }
        var entity = _userGroups.FirstOrDefault(ug => ug.Id == new UserGroupId(arg.Id));
        entity.NullCheck();
        entity.Modify(arg);
    }
    public async Task ModifyGroupPermission(ModifyGroupPermissionArg arg)
    {
        var entity = _groupPermissions.FirstOrDefault(gp => gp.Id == new GroupPermissionId(arg.Id));
        entity.NullCheck();
        entity.Modify(arg);
    }
    #endregion

    #region DeleteMethods
    public void DeleteUserGroup(long userGroupId, long loginUserId)
    {
        var entity = _userGroups.FirstOrDefault(g => g.Id == new UserGroupId(userGroupId));
        entity.NullCheck();
        entity?.Delete(loginUserId);
    }
    public void DeleteGroupPermission(long groupPermissionId, long loginUserId)
    {
        var entity = _groupPermissions.FirstOrDefault(gp => gp.Id == new GroupPermissionId(groupPermissionId));
        entity.NullCheck();
        entity?.Delete(loginUserId);
    }
    #endregion

    #region Guards
    private static async Task CreateGuards(CreateGroupArg arg, IGroupService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ActiveStatusId.NullCheck();

        if (!await service.IsCodeUnique(arg.Code, 0)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyGroupArg arg, IGroupService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (!await service.IsCodeUnique(arg.Code, arg.Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
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
    private List<ProjectGroup> _projectGroups = new();
    public ICollection<ProjectGroup> ProjectGroups => _projectGroups;
    private List<WorkFlowActorGroup> _workFlowActorGroups = new();
    public ICollection<WorkFlowActorGroup> WorkFlowActorGroups => _workFlowActorGroups;
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
