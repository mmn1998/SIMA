using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Args.Create;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Entites;

public partial class WorkFlowActorRole : Entity
{
    private WorkFlowActorRole()
    {

    }

    private WorkFlowActorRole(CreateWorkFlowActorRoleArg arg)
    {
        Id = new WorkFlowActorRoleId(IdHelper.GenerateUniqueId());
        WorkFlowActorId = arg.WorkFlowActorId;
        CreatedBy = arg.CreatedBy;
        CreatedAt = arg.CreatedAt;
        RoleId = arg.RoleId;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public static WorkFlowActorRole New(CreateWorkFlowActorRoleArg arg)
    {
        return new WorkFlowActorRole(arg);
    }
    public void Deactive()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Deactive;

    }
    public WorkFlowActorRoleId Id { get; set; }

    public WorkFlowActorId WorkFlowActorId { get; set; }

    public long RoleId { get; set; }

    public long? ActiveStatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public long? CreatedBy { get; set; }

    public byte[]? ModifiedAt { get; set; }

    public long? ModifiedBy { get; set; }

    public virtual WorkFlowActor WorkFlowActor { get; set; } = null!;
}
