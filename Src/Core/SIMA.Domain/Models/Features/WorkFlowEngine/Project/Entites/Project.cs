using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Args.Create;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Args.Modify;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.ValueObjects;
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
        DomainId = arg.DomainId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public static async Task<Project> New(CreateProjectArg arg)
    {
        return new Project(arg);
    }
    public void Modify(ModifyProjectArg arg)
    {
        Code = arg.Code;
        Name = arg.Name;
        DomainId = arg.DomainId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public void Deactive()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Deactive;
    }
    public bool DeactiveProjectGroup(long groupId)
    {
        var result = _projectGroups.Where(x => x.Id == new ProjectGroupId(groupId)).FirstOrDefault();
        if (result is not null)
        {
            result.Deactive();
            return true;
        }
        else
            return false;

    }
    public bool DeactiveProjectMmeber(long userId)
    {
        var result = _projectMember.Where(x => x.UserId == userId).FirstOrDefault();
        if (result is not null)
        {
            result.Deactive();
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
        var projectMember = request.Select(x => ProjectMember.New(new CreateProjectMemberArg { UserId = x.UserId, ProjectId = Id, IsAdminProject = x.IsAdminProject, IsManager = x.IsManager }));
        _projectMember.AddRange(projectMember);
    }
    public ProjectId Id { get; set; }
    public long? DomainId { get; set; }
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
