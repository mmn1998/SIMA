using SIMA.Domain.Models.Features.Auths.Groups.Entities;
using SIMA.Domain.Models.Features.Auths.Groups.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Args.Create;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Args.Modify;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.Project.Entites;

public class ProjectGroup : Entity
{
    private ProjectGroup()
    {

    }
    private ProjectGroup(CreateProjectGroupArg arg)
    {
        Id = new ProjectGroupId(IdHelper.GenerateUniqueId());
        ProjectId = arg.ProjectId;
        GroupId = new(arg.GroupId);
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public static ProjectGroup New(CreateProjectGroupArg arg)
    {
        return new ProjectGroup(arg);
    }

    public async Task Modify(ModifyProjectGroupArg arg)
    {
        ProjectId = new ProjectId(arg.ProjectId);
        GroupId= new(arg.GroupId);
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }

    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;

    }
    
    public ProjectGroupId Id { get; set; }
    public ProjectId ProjectId { get; set; }
    public virtual Project Project { get; set; }
    public GroupId GroupId { get; set; }
    public virtual Group Group { get; set; }
    public long? ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
