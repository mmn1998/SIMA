using SIMA.Domain.Models.Features.Auths.Users.Entities;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Args.Create;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Entites;

public partial class WorkFlowActorUser : Entity
{
    private WorkFlowActorUser()
    {

    }
    private WorkFlowActorUser(CreateWorkFlowActorUserArg arg)
    {
        Id = new WorkFlowActorUserId(IdHelper.GenerateUniqueId());
        WorkFlowActorId = arg.WorkFlowActorId;
        CreatedAt = DateTime.UtcNow;
        UserId = new(arg.UserId);
        ActiveStatusId = (long)ActiveStatusEnum.Active;

    }
    public static WorkFlowActorUser New(CreateWorkFlowActorUserArg arg)
    {
        return new WorkFlowActorUser(arg);
    }
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;

    }
    public WorkFlowActorUserId Id { get; set; }
    public WorkFlowActorId WorkFlowActorId { get; set; }
    public UserId UserId { get; set; }
    public virtual User User { get; set; }
    public long? ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
    public virtual WorkFlowActor WorkFlowActor { get; set; } = null!;
}
