using SIMA.Domain.Models.Features.Auths.Domains.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Args.Create;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Args.Modify;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Exceptions;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

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
    public static async Task<Project> New(CreateProjectArg arg)
    {
        return new Project(arg);
    }
    public async Task Modify(ModifyProjectArg arg)
    {
        Code = arg.Code;
        Name = arg.Name;
        if (arg.DomainId.HasValue) DomainId = new(arg.DomainId.Value);
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    public bool DeleteProjectGroup(long groupId)
    {
        var result = _projectGroups.Where(x => x.Id == new ProjectGroupId(groupId)).FirstOrDefault();
        if (result is not null)
        {
            result.Delete();
            return true;
        }
        else
            return false;

    }
    public bool DeleteProjectMmeber(long userId)
    {
        var result = _projectMember.Where(x => x.UserId == new UserId(userId)).FirstOrDefault();
        if (result is not null)
        {
            result.Delete();
            return true;
        }
        else
            return false;

    }
    public void AddProjectGroup(List<long> groupId)
    {
        var projectGroup = groupId.Select(x => ProjectGroup.New(new CreateProjectGroupArg { GroupId = x, ProjectId = Id, ActiveStatusId = (long)ActiveStatusEnum.Active }));
        _projectGroups.AddRange(projectGroup);
    }
    public void AddProjectMember(List<CreateProjectMemberArg> request)
    {
        var x = request.Count(x => x.IsManager == "1");
        if(x > 1) throw ProjectExceptions.ProjectMemberIsManagerError;
var projectMember = request.Select(x => ProjectMember.New(new CreateProjectMemberArg
        { 
            UserId = x.UserId,
            ProjectId = Id,
            IsAdminProject = x.IsAdminProject,
            IsManager = x.IsManager }
        ));
        _projectMember.AddRange(projectMember);
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
}
