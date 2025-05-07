using SIMA.Domain.Models.Features.Auths.Groups.Entities;
using SIMA.Domain.Models.Features.Auths.Groups.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Args.Create;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

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
        GroupId = new(arg.GroupId);
        ActiveStatusId = (long)ActiveStatusEnum.Active;
    }
    public static WorkFlowActorGroup New(CreateWorkFlowActorGroupArg arg)
    {
        return new WorkFlowActorGroup(arg);
    }

    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;

    }
    public WorkFlowActorGroupId Id { get; set; }
    public WorkFlowActorId WorkFlowActorId { get; set; }
    public virtual WorkFlowActor WorkFlowActor { get; set; }
    public GroupId GroupId { get; set; }
    public virtual Group Group { get; set; }
    public long? ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
