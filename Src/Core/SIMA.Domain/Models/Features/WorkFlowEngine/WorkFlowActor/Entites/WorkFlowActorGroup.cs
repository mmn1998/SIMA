using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Args.Create;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Entites;

public partial class WorkFlowActorGroup : Entity
{

    private WorkFlowActorGroup()
    {

    }
    private WorkFlowActorGroup(CreateWorkFlowActorGroupArg arg)
    {
        Id = new WorkFlowActorGroupId(IdHelper.GenerateUniqueId()); 
        WorkFlowActorId = arg.WorkFlowActorId;
        CreatedAt = DateTime.UtcNow;
        GroupId = arg.GroupId;
        ActiveStatusId = (long)ActiveStatusEnum.Active;
    }
    public static WorkFlowActorGroup New(CreateWorkFlowActorGroupArg arg)
    {
        return new WorkFlowActorGroup(arg);
    }

    public void Deactive()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Deactive;

    }
    public WorkFlowActorGroupId Id { get; set; }
    public WorkFlowActorId WorkFlowActorId { get; set; }
    public long GroupId { get; set; }
    public long? ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
