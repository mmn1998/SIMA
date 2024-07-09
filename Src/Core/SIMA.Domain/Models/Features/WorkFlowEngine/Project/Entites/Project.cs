using SIMA.Domain.Models.Features.Auths.Domains.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Groups.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Args.Create;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Args.Modify;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Interface;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.Project.Entites;

public partial class Project : Entity
{
    private Project()
    {

    }
    private Project(CreateProjectArg arg)
    {
        Id = new ProjectId(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
        if (arg.DomainId.HasValue) DomainId = new(arg.DomainId.Value);
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public static async Task<Project> New(CreateProjectArg arg , IProjectDomainService service)
    {
        await CreateGuard(arg, service);
        return new Project(arg);
    }
    public async Task Modify(ModifyProjectArg arg, IProjectDomainService service)
    {
        await ModifyGuard(arg, service);
        Code = arg.Code;
        Name = arg.Name;
        if (arg.DomainId.HasValue) DomainId = new(arg.DomainId.Value);
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    public bool DeleteProjectGroup(long groupId, long loginUserId)
    {
        var result = _projectGroups.Where(x => x.Id == new ProjectGroupId(groupId)).FirstOrDefault();
        if (result is not null)
        {
            result.Delete(loginUserId);
            return true;
        }
        else
            return false;

    }
    public bool DeleteProjectMmeber(long userId, long loginUserId)
    {
        var result = _projectMember.Where(x => x.UserId == new UserId(userId)).FirstOrDefault();
        if (result is not null)
        {
            result.Delete(loginUserId);
            return true;
        }
        else
            return false;

    }
    public void AddProjectGroup(List<CreateProjectGroupArg> groups, long ProjectId)
    {
        var previousProjectGroups = _projectGroups.Where(x => x.ProjectId == new ProjectId(ProjectId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

        var addGroup = groups.Where(x => !previousProjectGroups.Any(c => c.GroupId.Value == x.GroupId)).ToList();
        var deleteGroup = previousProjectGroups.Where(x => !groups.Any(c => c.GroupId == x.GroupId.Value)).ToList();


        foreach (var group in addGroup)
        {
            var entity = _projectGroups.Where(x => (x.GroupId == new GroupId(group.GroupId) && x.ProjectId == new ProjectId(ProjectId)) && x.ActiveStatusId != (long)ActiveStatusEnum.Active).FirstOrDefault();
            if (entity is not null)
            {
                entity.ActiveStatusId = (long)ActiveStatusEnum.Active;
            }
            else
            {
                entity = ProjectGroup.New(group);
                _projectGroups.Add(entity);
            }
        }

        foreach (var group in deleteGroup)
        {
            group.ActiveStatusId = (long)ActiveStatusEnum.Delete;
        }
    }
    public void AddProjectMember(List<CreateProjectMemberArg> request, long projectId)
    {
        var checkManager = request.Count(x => x.IsManager == "1");
        if (checkManager > 1) throw new SimaResultException(CodeMessges._400Code , Messages.ProjectMemberIsManagerError);

        var previousProjectMembers = _projectMember.Where(x => x.ProjectId == new ProjectId(projectId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

        var addMember = request.Where(x => !previousProjectMembers.Any(c => c.UserId.Value == x.UserId)).ToList();
        var deleteMember = previousProjectMembers.Where(x => !request.Any(c => c.UserId == x.UserId.Value)).ToList();


        foreach (var member in addMember)
        {
            var entity = _projectMember.Where(x => (x.UserId == new UserId(member.UserId) && x.ProjectId == new ProjectId(projectId)) && x.ActiveStatusId != (long)ActiveStatusEnum.Active).FirstOrDefault();
            if (entity is not null)
            {
                entity.ActiveStatusId = (long)ActiveStatusEnum.Active;
            }
            else
            {
                entity = ProjectMember.New(member);
                _projectMember.Add(entity);
            }
        }

        foreach (var member in deleteMember)
        {
            member.ActiveStatusId = (long)ActiveStatusEnum.Delete;
        }
    }
    public ProjectId Id { get; set; }
    public DomainId? DomainId { get; set; }
    public virtual Auths.Domains.Entities.Domain? Domain { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long? ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }

    private List<ProjectGroup> _projectGroups = new();
    public List<ProjectGroup> ProjectGroups => _projectGroups;
    private List<ProjectMember> _projectMember = new();
    public virtual List<ProjectMember> ProjectMembers => _projectMember;
    public virtual ICollection<WorkFlow.Entities.WorkFlow> WorkFlows { get; set; } = new List<WorkFlow.Entities.WorkFlow>();


    #region Guards
    private static async Task CreateGuard(CreateProjectArg arg, IProjectDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code,Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code,Messages.LengthCodeException);
        if (await service.IsCodeUnique(arg.Code, 0)) throw new SimaResultException(CodeMessges._400Code,Messages.UniqueCodeError);
    }

    private async Task ModifyGuard(ModifyProjectArg arg, IProjectDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code,Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code,Messages.LengthCodeException);
        if (await service.IsCodeUnique(arg.Code, Id.Value)) throw new SimaResultException(CodeMessges._400Code,Messages.UniqueCodeError);
    }
    #endregion
}
