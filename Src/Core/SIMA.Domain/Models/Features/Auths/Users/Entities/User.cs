using SIMA.Application.Query.Contract.Features.Auths.Users;
using SIMA.Domain.Models.Features.Auths.AdminLocationAccesses.Entities;
using SIMA.Domain.Models.Features.Auths.Companies.Entities;
using SIMA.Domain.Models.Features.Auths.Companies.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Forms.Entities;
using SIMA.Domain.Models.Features.Auths.Groups.Args;
using SIMA.Domain.Models.Features.Auths.Groups.Entities;
using SIMA.Domain.Models.Features.Auths.Groups.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Permissions.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Profiles.Entities;
using SIMA.Domain.Models.Features.Auths.Profiles.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Roles.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.Args;
using SIMA.Domain.Models.Features.Auths.Users.Exceptions;
using SIMA.Domain.Models.Features.Auths.Users.Interfaces;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Approvals.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Subjects.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Entites;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Entites;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;

namespace SIMA.Domain.Models.Features.Auths.Users.Entities;

public class User : Entity, IAggregateRoot
{
    // ValueObject
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
        }
        #region AddMethods
        public async Task AddUserRole(CreateUserRoleArg arg)
        {
            if (_userRoles.Any(ur => ur.RoleId == new RoleId(arg.RoleId) && ur.UserId == new UserId(arg.UserId)))
            {
                throw new SimaResultException("10017",Messages.UserRoleDuplicateError);
            }
            var entity = await UserRole.Create(arg);
            _userRoles.Add(entity);

    }
    public async Task AddUserPermission(CreateUserPermissionArg arg)
    {
        arg.UserId.NullCheck();
        if (_userPermission.Any(ur => ur.PermissionId == new PermissionId(arg.PermissionId) && ur.UserId == new UserId(arg.UserId.Value)))
        {
            throw new SimaResultException("10028", Messages.UserPermoissionDuplicateError);
        }
        var entity = await UserPermission.Create(arg);
        _userPermission.Add(entity);
    }
    public async Task AddUserDomain(CreateUserDomainArg arg)
    {
        var entity = await UserDomainAccess.Create(arg);
        if (_userDomainAccesses.Contains(entity))
        {
            //throw new SimaResultException(CodeMessges._400Code,Messages.
        }
        else
        {
            _userDomainAccesses.Add(entity);
        }

    }
    public async Task AddUserLocation(CreateUserLocationAccessArg arg)
    {
        var entity = await UserLocationAccess.Create(arg);
        _userLocationAccesses.Add(entity);
    }
    public async Task AddUserLocations(List<CreateUserLocationAccessArg> args)
    {
        foreach (var arg in args)
        {
            await AddUserLocation(arg);
        }
    }
    public async Task AddUserDomains(List<CreateUserDomainArg> args)
    {
        foreach (var arg in args)
        {
            await AddUserDomain(arg);
        }
    }
    public async Task AddUserPermissions(List<CreateUserPermissionArg> args)
    {
        foreach (var arg in args)
        {
            await AddUserPermission(arg);
        }
    }
    public async Task AddUserRoles(List<CreateUserRoleArg> args)
    {
        foreach (var arg in args)
        {
            await AddUserRole(arg);
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
        Username = arg.Username;
        Password = PasswordValueObject.New(arg.Password);
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

        public async Task AccessFaild(UserInfoLogin arg)
        {
            AccessFailedCount = arg.AccessFailedCount;
            AccessFaildDate = arg.AccessFaildDate;
            AccessFailedOverallCount = arg.AccessFailedOverallCount;
            IsLocked = arg.IsLocked;
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
        public DateTime? ChangePasswordDate { get; private set; }
        public int? AccessFailedCount { get; private set; }
        public int? AccessFailedOverallCount { get; private set; }
        public DateTime? AccessFaildDate { get; private set; }
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
                throw new SimaResultException("10017",Messages.UserRoleDuplicateError);
            }
            var entity = _userRoles.FirstOrDefault(ur => ur.Id == new UserRoleId(arg.Id));
            entity.NullCheck();
            entity.Modify(arg);
        }
        public async Task ModifyUserPermission(ModifyUserPermissionArg arg)
        {
            if (_userPermission.Any(ur => ur.PermissionId == new PermissionId(arg.PermissionId) && ur.UserId == new UserId(arg.UserId.Value)))
            {
                throw new SimaResultException("10028",Messages.UserPermoissionDuplicateError);
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
        public async Task ModifyUserDomain(ModifyUserDomainArg arg)
        {

        var entity = _userDomainAccesses.FirstOrDefault(ud => ud.Id == new UserDomainAccessId(arg.Id));
        entity.NullCheck();
        await entity.Modify(arg);
    }
    #endregion

    #region DeleteMethods
    public void DeleteUserConfig(long userConfigId)
    {
        var entity = _usreConfig.FirstOrDefault(uc => uc.Id == new UserConfigId(userConfigId));
        entity.NullCheck();
        entity?.Delete();
    }
    public void DeleteUserDomainAccess(long UserDomainAccessId)
    {
        var entity = _userDomainAccesses.FirstOrDefault(uc => uc.Id == new UserDomainAccessId(UserDomainAccessId));
        entity.NullCheck();
        entity?.Delete();
    }
    public void DeleteUserLocationAccess(long UserLocationId)
    {
        var entity = _userLocationAccesses.FirstOrDefault(uc => uc.Id == new UserLocationAccessId(UserLocationId));
        entity.NullCheck();
        entity?.Delete();
    }
    public void DeleteUserPermission(long userPermissionId)
    {
        var entity = _userPermission.FirstOrDefault(uc => uc.Id == new UserPermissionId(userPermissionId));
        entity.NullCheck();
        entity?.Delete();
    }
    public void DeleteUserRole(long userRoleId)
    {
        var entity = _usreConfig.FirstOrDefault(uc => uc.Id == new UserConfigId(userRoleId));
        entity.NullCheck();
        entity?.Delete();
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
    private List<UserDomainAccess> _userDomainAccesses = new();
    public ICollection<UserDomainAccess> UserDomainAccesses => _userDomainAccesses;
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
        private static async Task CreateGuards(IUserService userService, CreateUserArg arg)
        {
            arg.Password.NullCheck();
            arg.Username.NullCheck();
            if (!userService.IsUsernameValidRegex(arg.Username)) throw new  SimaResultException(CodeMessges._400Code, Messages.UsernameNotValidException);
            if (!await userService.IsUsernameUnique(arg.Username)) throw new SimaResultException(CodeMessges._400Code, Messages.UsernameNotUniqueException);
        }
        private async Task ModifyGuards(IUserService userService, ModifyUserArg arg)
        {
            arg.Password.NullCheck();
            arg.Username.NullCheck();
            if (!userService.IsUsernameValidRegex(arg.Username)) throw new SimaResultException(CodeMessges._400Code, Messages.UsernameNotValidException);
            if (!await userService.IsUsernameUnique(arg.Username)) throw new SimaResultException(CodeMessges._400Code, Messages.UsernameNotUniqueException);
        }

        private async Task ChangePasswordGuards(ChangePasswordArg request)
        {
            if (request.NewPassword is null) throw new SimaResultException(CodeMessges._400Code, Messages.NewPasswordIsNull);
            if (request.ConfirmNewPassword is null) throw new SimaResultException(CodeMessges._400Code, Messages.ConfirmNewPasswordIsNull);
            if(request.NewPassword != request.ConfirmNewPassword) throw new SimaResultException(CodeMessges._400Code, Messages.ConfirmPasswordNotValid);
        }

}
