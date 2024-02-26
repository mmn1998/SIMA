using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Args.Create;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Args.Modify;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.Project.Entites;

public partial class ProjectMember : Entity
{

    private ProjectMember()
    {

    }
    private ProjectMember(CreateProjectMemberArg arg)
    {
        Id = new ProjectMemberId(IdHelper.GenerateUniqueId());
        ProjectId = arg.ProjectId;
        UserId = arg.UserId;
        IsAdminProject = arg.IsAdminProject;
        IsManager = arg.IsManager;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static ProjectMember New(CreateProjectMemberArg arg)
    {
        return new ProjectMember(arg);
    }

    public void Modify(ModifyProjectMemberArg arg)
    {
        ProjectId = new ProjectId(arg.ProjectId);
        UserId = arg.UserId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }

    public void Deactive()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Deactive;

    }
   
    public ProjectMemberId Id { get; set; }
    public ProjectId ProjectId { get; set; }
    public long UserId { get; set; }
    public string IsManager { get; set; } = null!;
    public string IsAdminProject { get; set; } = null!;
    public long? ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
    public virtual Project Project { get; set; } = null!;
}
