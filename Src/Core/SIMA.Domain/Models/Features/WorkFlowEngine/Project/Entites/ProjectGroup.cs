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
        GroupId = arg.GroupId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public static ProjectGroup New(CreateProjectGroupArg arg)
    {
        return new ProjectGroup(arg);
    }

    public void Modify(ModifyProjectGroupArg arg)
    {
        ProjectId = new ProjectId(arg.ProjectId);
        GroupId= arg.GroupId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }

    public void Deactive()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Deactive;

    }
    
    public ProjectGroupId Id { get; set; }
    public ProjectId ProjectId { get; set; }
    public long GroupId { get; set; }
    public long? ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
