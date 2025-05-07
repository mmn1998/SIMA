using SIMA.Application.Query.Contract.Features.Auths.Users;
using SIMA.Domain.Models.Features.Auths.AdminLocationAccesses.Entities;
using SIMA.Domain.Models.Features.Auths.Companies.Entities;
using SIMA.Domain.Models.Features.Auths.Companies.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Domains.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Forms.Entities;
using SIMA.Domain.Models.Features.Auths.Forms.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Groups.Args;
using SIMA.Domain.Models.Features.Auths.Groups.Entities;
using SIMA.Domain.Models.Features.Auths.Groups.Interfaces;
using SIMA.Domain.Models.Features.Auths.Groups.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Locations.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Permissions.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Profiles.Entities;
using SIMA.Domain.Models.Features.Auths.Profiles.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Roles.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.Args;
using SIMA.Domain.Models.Features.Auths.Users.Interfaces;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Approvals.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Subjects.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Entites;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Entites;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.Auths.Users.Entities;

public class User : Entity, IAggregateRoot
{
    private User()
    {
    }
    private User(CreateUserArg arg)
    {
        Id = new UserId(IdHelper.GenerateUniqueId());
        ProfileId = new ProfileId(arg.ProfileId);
        CompanyId = new CompanyId(arg.CompanyId);
        Username = arg.Username;
        Password = PasswordValueObject.New(arg.Password);
        ActiveStatusId = arg.ActiveStatusId;
        ActiveFrom = arg.ActiveFrom;
        CreatedBy = arg.CreatedBy;
        CreatedAt = arg.CreatedAt;
        IsFirstLogin = arg.IsFirstLogin;
        IsLocked = arg.IsLocked;
        IsSendOTP = arg.IsSendOTP;
    }
    #region AddMethods

    #region old Method

    //public async Task AddUserRole(CreateUserRoleArg arg)
    //{
    //    if (_userRoles.Any(ur => ur.RoleId == new RoleId(arg.RoleId) && ur.UserId == new UserId(arg.UserId)))
    //    {
    //        throw new SimaResultException(CodeMessges._100017Code, Messages.UserRoleDuplicateError);
    //    }
    //    var entity = await UserRole.Create(arg);
    //    _userRoles.Add(entity);

    //}
    //public async Task AddUserPermission(CreateUserPermissionArg arg)
    //{
    //    arg.UserId.NullCheck();
    //    if (_userPermission.Any(ur => ur.PermissionId == new PermissionId(arg.PermissionId) && ur.UserId == new UserId(arg.UserId.Value)))
    //    {
    //        throw new SimaResultException(CodeMessges._100028Code, Messages.UserPermoissionDuplicateError);
    //    }
    //    var entity = await UserPermission.Create(arg);
    //    _userPermission.Add(entity);
    //}
    //public async Task AddUserDomain(CreateUserDomainArg arg)
    //{
    //    var entity = await UserDomainAccess.Create(arg);
    //    if (_userDomainAccesses.Contains(entity))
    //    {
    //        //throw new SimaResultException(CodeMessges._400Code,Messages.
    //    }
    //    else
    //    {
    //        _userDomainAccesses.Add(entity);
    //    }
    //}

    //public async Task AddUserLocation(CreateUserLocationAccessArg arg)
    //{
    //    var entity = await UserLocationAccess.Create(arg);
    //    _userLocationAccesses.Add(entity);
    //}
    //public async Task AddUserLocations(List<CreateUserLocationAccessArg> args)
    //{
    //    foreach (var arg in args)
    //    {
    //        await AddUserLocation(arg);
    //    }
    //}
    //public async Task AddUserDomains(List<CreateUserDomainArg> args)
    //{
    //    foreach (var arg in args)
    //    {
    //        await AddUserDomain(arg);
    //    }
    //}
    //public async Task AddUserPermissions(List<CreateUserPermissionArg> args)
    //{
    //    foreach (var arg in args)
    //    {
    //        await AddUserPermission(arg);
    //    }
    //}
    //public async Task AddUserRoles(List<CreateUserRoleArg> args)
    //{
    //    foreach (var arg in args)
    //    {
    //        await AddUserRole(arg);
    //    }
    //}

    #endregion

    public async Task AddUserPermission(List<CreateUserPermissionArg> request, long userId)
     {
        userId.NullCheck();

        var previousUsers = _userPermission.Where(x => x.UserId == new UserId(userId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

        var addPermission = request.Where(x => !previousUsers.Any(c => c.PermissionId.Value == x.PermissionId)).ToList();

        var deleteMember = previousUsers.Where(x => !request.Any(c => c.PermissionId == x.PermissionId.Value)).ToList();


        //var toBeDeleted = previousUsers.Select(x => x.PermissionId.Value).Except(request.Select(x => x.PermissionId));

        //var xxx = toBeDeleted.Count();

        //var deeee = _userPermission.Where(x => x.UserId.Value == userId && toBeDeleted.Contains(x.PermissionId.Value)).ToList();
        //var demo = request;

        //var deleted = new List<UserPermission>();
        //foreach (var item in previousUsers.OrderBy(f=>f.PermissionId.Value))
        //{
        //    foreach (var item1 in demo.OrderBy(f=>f.PermissionId))
        //    {
        //        if(item.PermissionId.Value == item1.PermissionId)
        //        {
        //            break;
        //        }
        //        else
        //        {
        //            deleted.Add(item);
        //        }
        //    }
        //} 



        foreach (var permission in addPermission)
        {

            var entity = await UserPermission.Create(permission);
            _userPermission.Add(entity);
        }

        foreach (var permission in deleteMember)
        {
            permission.Delete((long)request[0].CreatedBy);
        }
    }
    public async Task AddFormUser(List<CreateFormUserArg> request, long userId, IUserService service)
    {
        userId.NullCheck();

        var previousUsers = _formUsers.Where(x => x.UserId == new UserId(userId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

        var addForm = request.Where(x => !previousUsers.Any(c => c.FormId.Value == x.FormId)).ToList();
        var deleteForm = previousUsers.Where(x => !request.Any(c => c.FormId == x.FormId.Value)).ToList();

        foreach (var form in addForm)
        {
            var entity = _formUsers.Where(x => (x.FormId == new FormId(form.FormId.Value) && x.UserId == new UserId(userId)) && x.ActiveStatusId != (long)ActiveStatusEnum.Active).FirstOrDefault();
            if (entity is not null)
            {
                await entity.ChangeStatus(ActiveStatusEnum.Active);
            }
            else
            {
                entity = await FormUser.Create(form);
                _formUsers.Add(entity);
            }
        }

        if (deleteForm is not null && deleteForm.Count > 0)
        {
            foreach (var form in deleteForm)
            {
                form.Delete((long)request[0].CreatedBy);
                var userPermissionIds = await ChangeFormAccessGuards(form.FormId.Value, userId, service);
                foreach (var permission in userPermissionIds)
                {
                    var entity = _userPermission.Where(x => x.PermissionId.Value == permission && x.ActiveStatusId != 3).FirstOrDefault();
                    entity.Delete((long)request[0].CreatedBy);
                }
            }
        }
    }
    public async Task AddUserRole(List<CreateUserRoleArg> request, long userId)
    {
        userId.NullCheck();

        var previousRoles = _userRoles.Where(x => x.UserId == new UserId(userId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

        var addRole = request.Where(x => !previousRoles.Any(c => c.RoleId.Value == x.RoleId)).ToList();
        var deleteMember = previousRoles.Where(x => !request.Any(c => c.RoleId == x.RoleId.Value)).ToList();


        foreach (var role in addRole)
        {
            var entity = _userRoles.Where(x => (x.RoleId == new RoleId(role.RoleId) && x.UserId == new UserId(userId)) && x.ActiveStatusId != (long)ActiveStatusEnum.Active).FirstOrDefault();
            if (entity is not null)
            {
                await entity.ChangeStatus(ActiveStatusEnum.Active);
            }
            else
            {
                entity = await UserRole.Create(role);
                _userRoles.Add(entity);
            }
        }

        foreach (var role in deleteMember)
        {
            role.Delete((long)request[0].CreatedBy);
        }
    }

    public async Task AddUserGroup(List<CreateUserGroupArg> request, long userId)
    {
        userId.NullCheck();

        var previousGroups = _userGroup.Where(x => x.UserId == new UserId(userId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

        var addGroup = request.Where(x => !previousGroups.Any(c => c.GroupId.Value == x.GroupId)).ToList();
        var deleteMember = previousGroups.Where(x => !request.Any(c => c.GroupId == x.GroupId.Value)).ToList();


        foreach (var group in addGroup)
        {
            var entity = _userGroup.Where(x => (x.GroupId == new GroupId(group.GroupId.Value) && x.UserId == new UserId(userId)) && x.ActiveStatusId != (long)ActiveStatusEnum.Active).FirstOrDefault();
            if (entity is not null)
            {
                await entity.ChangeStatus(ActiveStatusEnum.Active);
            }
            else
            {
                entity = await UserGroup.Create(group);
                _userGroup.Add(entity);
            }
        }

        foreach (var group in deleteMember)
        {
            group.Delete((long)request[0].CreatedBy);
        }
    }
    public async Task AddUserLocation(List<CreateUserLocationAccessArg> request, long userId)
    {
        userId.NullCheck();

        var previousLocations = _userLocationAccesses.Where(x => x.UserId == new UserId(userId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

        var addLocation = request.Where(x => !previousLocations.Any(c => c.LocationId.Value == x.LocationId)).ToList();
        var deleteLocation = previousLocations.Where(x => !request.Any(c => c.LocationId == x.LocationId.Value)).ToList();


        foreach (var domain in addLocation)
        {
            var entity = _userLocationAccesses.Where(x => (x.LocationId == new LocationId((long)domain.LocationId) && x.UserId == new UserId(userId)) && x.ActiveStatusId != (long)ActiveStatusEnum.Active).FirstOrDefault();
            if (entity is not null)
            {
                await entity.ChangeStatus(ActiveStatusEnum.Active);
            }
            else
            {
                entity = await UserLocationAccess.Create(domain);
                _userLocationAccesses.Add(entity);
            }
        }

        foreach (var domain in deleteLocation)
        {
            domain.Delete((long)request[0].CreatedBy);
        }
    }
    
    #endregion

    public void Delete()
    {
        ActiveTo = DateOnly.FromDateTime(DateTime.Now);
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }

    public void Activate()
    {
        ActiveFrom = DateOnly.FromDateTime(DateTime.Now);
        ActiveStatusId = (long)ActiveStatusEnum.Active; ;
    }
    public static async Task<User> Create(IUserService service, CreateUserArg arg)
    {
        await CreateGuards(service, arg);
        return new User(arg);
    }
    public async Task Modify(ModifyUserArg arg, IUserService userService)
    {
        await ModifyGuards(userService, arg);
        if (arg.ProfileId.HasValue) ProfileId = new ProfileId(arg.ProfileId.Value);
        if (arg.CompanyId.HasValue) CompanyId = new CompanyId(arg.CompanyId.Value);
        Username = arg.Username;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
        ActiveStatusId = arg.ActiveStatusId;
    }

    public async Task ChangePassword(ChangePasswordArg arg)
    {
        await ChangePasswordGuards(arg);
        Password = PasswordValueObject.New(arg.NewPassword);
        IsFirstLogin = arg.IsFirstLogin;
        ChangePasswordDate = arg.ChangePasswordDate;
        AccessFailedCount = arg.AccessFailedCount;
    }

    public async Task<string> GenerateCode(IUserService userService)
    {
        var code = userService.GenerateCode();
        ConfirmCode = code;
        ConfirmCodeSendDate = DateTime.Now;
        return code;
    }

    public string GeneratePassword(IUserService userService)
    {
        string newPassword = userService.GeneratePassword();
        Password = PasswordValueObject.New(newPassword);
        ConfirmCode = null;
        ConfirmCodeSendDate = null;
        IsFirstLogin = "1";
        ChangePasswordDate = DateTime.Now;
        return newPassword;
    }

    public void ConfirmOTpCode()
    {
        ConfirmCode = null;
        ConfirmCodeSendDate = null;
    }

    public void AccessFailed(UserInfoLogin arg)
    {
        AccessFailedCount = arg.AccessFailedCount;
        AccessFailedDate = arg.AccessFailedDate;
        AccessFailedOverallCount = arg.AccessFailedOverallCount;
        IsLocked = arg.IsLocked;
    }
    public void ResetWrongPassActivity()
    {
        AccessFailedCount = 0;
    }
    public string? SecretKey { get; set; }
    public ProfileId? ProfileId { get; private set; }
    public CompanyId CompanyId { get; private set; }
    public long ActiveStatusId { get; private set; }
    public string Username { get; private set; }
    public PasswordValueObject Password { get; private set; }
    public string? IsLoggedIn { get; private set; }
    public DateOnly? ActiveFrom { get; private set; }
    public DateOnly? ActiveTo { get; private set; }
    public UserId Id { get; private set; }
    public string IsFirstLogin { get; set; }
    public string IsLocked { get; set; }
    public string IsSendOTP { get; set; }
    public DateTime? ChangePasswordDate { get; private set; }
    public int? AccessFailedCount { get; private set; }
    public int? AccessFailedOverallCount { get; private set; }
    public DateTime? AccessFailedDate { get; private set; }
    public string? ConfirmCode { get; private set; }
    public DateTime? ConfirmCodeSendDate { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public virtual Company? Company { get; private set; }
    public virtual Profile? Profile { get; private set; }

    #region ModifyMethods
    public async Task ModifyUserRole(ModifyUserRoleArg arg)
    {
        if (_userRoles.Any(ur => ur.RoleId == new RoleId(arg.RoleId) && ur.UserId == new UserId(arg.UserId)))
        {
            throw new SimaResultException(CodeMessges._100017Code, Messages.UserRoleDuplicateError);
        }
        var entity = _userRoles.FirstOrDefault(ur => ur.Id == new UserRoleId(arg.Id));
        entity.NullCheck();
        entity.Modify(arg);
    }
    public async Task ModifyUserPermission(ModifyUserPermissionArg arg)
    {
        if (_userPermission.Any(ur => ur.PermissionId == new PermissionId(arg.PermissionId) && ur.UserId == new UserId(arg.UserId.Value)))
        {
            throw new SimaResultException(CodeMessges._100028Code, Messages.UserPermoissionDuplicateError);
        }
        var entity = _userPermission.FirstOrDefault(up => up.Id == new UserPermissionId(arg.Id));
        entity.NullCheck();
        entity.Modify(arg);
    }
    public async Task ModifyUserLocation(ModifyUserLocationArg arg)
    {
        var entity = _userLocationAccesses.FirstOrDefault(ul => ul.Id == new UserLocationAccessId(arg.Id));
        entity.NullCheck();
        entity.Modify(arg);
    }
    public async Task ModifyUserGroup(ModifyUserGroupArg arg)
    {
        var entity = _userGroup.FirstOrDefault(ug => ug.Id == new UserGroupId(arg.Id));
        entity.NullCheck();
        entity.Modify(arg);
    }
    #endregion

    #region DeleteMethods
    public void DeleteUserConfig(long userConfigId, long userId)
    {
        var entity = _usreConfig.FirstOrDefault(uc => uc.Id == new UserConfigId(userConfigId));
        entity.NullCheck();
        entity?.Delete(userId);
    }
   
    public void DeleteUserLocationAccess(long UserLocationId, long userId)
    {
        var entity = _userLocationAccesses.FirstOrDefault(uc => uc.Id == new UserLocationAccessId(UserLocationId));
        entity.NullCheck();
        entity?.Delete(userId);
    }
    public void DeleteUserPermission(long userPermissionId, long userId)
    {
        var entity = _userPermission.FirstOrDefault(uc => uc.Id == new UserPermissionId(userPermissionId));
        entity.NullCheck();
        entity?.Delete(userId);
    }
    public void DeleteUserRole(long userRoleId, long userId)
    {
        var entity = _usreConfig.FirstOrDefault(uc => uc.Id == new UserConfigId(userRoleId));
        entity.NullCheck();
        entity?.Delete(userId);
    }
    #endregion
    private List<UserConfig> _usreConfig = new();

    public ICollection<UserConfig> UserConfigs => _usreConfig;
    private List<UserGroup> _userGroup = new();

    public ICollection<UserGroup> UserGroups => _userGroup;
    private List<UserPermission> _userPermission = new();

    public ICollection<UserPermission> UserPermissions => _userPermission;
    private List<UserRole> _userRoles = new();

    public virtual ICollection<UserRole> UserRoles => _userRoles;
    private List<UserLocationAccess> _userLocationAccesses = new();
    public ICollection<UserLocationAccess> UserLocationAccesses => _userLocationAccesses;

    private List<AdminLocationAccess> _adminLocationAccesses = new();
    private List<FormUser> _formUsers = new();
    public ICollection<FormUser> FormUsers => _formUsers;
    private List<IssueHistory> _issueHistories = new();
    public ICollection<IssueHistory> IssueHistories => _issueHistories;
    private List<WorkFlowActorUser> _workFlowActorUsers = new();
    public ICollection<WorkFlowActorUser> WorkFlowActorUsers => _workFlowActorUsers;
    private List<ProjectMember> _projectMembers = new();
    public ICollection<ProjectMember> ProjectMembers => _projectMembers;
    private List<Subject> _subjects = new();
    public ICollection<Subject> Subjects => _subjects;
    private List<Approval> _approvals = new();
    public ICollection<Approval> Approvals => _approvals;
    private List<SubjectMeeting> _subjectMeetings = new();
    public ICollection<SubjectMeeting> SubjectMeetings => _subjectMeetings;

    public ICollection<AdminLocationAccess> AdminLocationAccesses => _adminLocationAccesses;

    private List<IssueManager> _issueManager = new();
    public ICollection<IssueManager> IssueManagers => _issueManager;

    private List<Issue> _issues = new();
    public ICollection<Issue> Issues => _issues;


    private static async Task CreateGuards(IUserService userService, CreateUserArg arg)
    {
        arg.Password.NullCheck();
        arg.Username.NullCheck();
        if (!userService.IsUsernameValidRegex(arg.Username)) throw new SimaResultException(CodeMessges._400Code, Messages.UsernameNotValidException);
        if (!await userService.IsUsernameUnique(arg.Username , 0)) throw new SimaResultException(CodeMessges._400Code, Messages.UsernameNotUniqueException);
    }
    private async Task ModifyGuards(IUserService userService, ModifyUserArg arg)
    {
        arg.Username.NullCheck();
        if (!userService.IsUsernameValidRegex(arg.Username)) throw new SimaResultException(CodeMessges._400Code, Messages.UsernameNotValidException);
        if (!await userService.IsUsernameUnique(arg.Username , arg.Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UsernameNotUniqueException);
    }

    private async Task ChangePasswordGuards(ChangePasswordArg request)
    {
        if (request.NewPassword is null) throw new SimaResultException(CodeMessges._400Code, Messages.NewPasswordIsNull);
        if (request.ConfirmNewPassword is null) throw new SimaResultException(CodeMessges._400Code, Messages.ConfirmNewPasswordIsNull);
        if (request.NewPassword != request.ConfirmNewPassword) throw new SimaResultException(CodeMessges._400Code, Messages.ConfirmPasswordNotValid);
    }

    private async Task<List<long>> ChangeFormAccessGuards(long fromId, long userId, IUserService service)
    {
        var UserPermissionIds = await service.GetUserPermissonByFormId(fromId , userId);
        return UserPermissionIds;
    }

    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
