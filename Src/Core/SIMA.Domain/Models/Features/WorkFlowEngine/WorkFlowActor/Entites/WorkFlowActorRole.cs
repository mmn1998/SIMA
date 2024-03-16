using SIMA.Domain.Models.Features.Auths.Roles.Entities;
using SIMA.Domain.Models.Features.Auths.Roles.ValueObjects;
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
        RoleId = new(arg.RoleId);
        ActiveStatusId = arg.ActiveStatusId;
    }
    public static WorkFlowActorRole New(CreateWorkFlowActorRoleArg arg)
    {
        return new WorkFlowActorRole(arg);
    }
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;

    }
    public WorkFlowActorRoleId Id { get; set; }

    public WorkFlowActorId WorkFlowActorId { get; set; }

    public RoleId RoleId { get; set; }
    public virtual Role Role { get; set; }

    public long? ActiveStatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public long? CreatedBy { get; set; }

    public byte[]? ModifiedAt { get; set; }

    public long? ModifiedBy { get; set; }

    public virtual WorkFlowActor WorkFlowActor { get; set; } = null!;
}
