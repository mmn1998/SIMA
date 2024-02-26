using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Args.Create;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Args.Modify;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Entites;

public class WorkFlowActor : Entity
{
    private WorkFlowActor()
    {

    }
    private WorkFlowActor(CreateWorkFlowActorArg arg)
    {
        Id = new WorkFlowActorId(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        CreatedBy = arg.CreatedBy;
        CreatedAt = arg.CreatedAt;
        WorkFlowId = new WorkFlowId(arg.WorkFlowId);
        ActiveStatusId = arg.ActiveStatusId;
    }
    public static WorkFlowActor New(CreateWorkFlowActorArg arg)
    {
        var result = new WorkFlowActor(arg);
        return result;
    }

    public void Modify(ModifyWorkFlowActorArg arg)
    {
        Code = arg.Code;
        Name = arg.Name;
        WorkFlowId = new WorkFlowId(arg.WorkFlowId);
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }

    public void Deactive()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Deactive;
    }

    public void AddActorRole(List<long> roleId)
    {
        var actorRoles = roleId.Select(x => WorkFlowActorRole.New(new CreateWorkFlowActorRoleArg { RoleId = x, WorkFlowActorId = Id }));
        foreach (var actorRole in actorRoles)
        {
            if (!_workFlowActorRoles.Any(war => war.WorkFlowActorId == Id && war.RoleId == actorRole.RoleId))
            {
                _workFlowActorRoles.Add(actorRole);
            }
        }
    }
    public void AddActorUser(List<long> userId)
    {
        var actorUsers = userId.Select(x => WorkFlowActorUser.New(new CreateWorkFlowActorUserArg { UserId = x, WorkFlowActorId = Id }));
        foreach (var actorUser in actorUsers)
        {
            if (!_workFlowActorUsers.Any(wau => wau.WorkFlowActorId == Id && wau.UserId == actorUser.UserId))
            {
                _workFlowActorUsers.Add(actorUser);
            }
        }
    }
    public void AddActorGroup(List<long> groupId)
    {
        var actorGroups = groupId.Select(x => WorkFlowActorGroup.New(new CreateWorkFlowActorGroupArg { GroupId = x, WorkFlowActorId = Id }));
        foreach (var actorGroup in actorGroups)
        {
            if (!_workFlowActorGroups.Any(wag => wag.WorkFlowActorId == Id && wag.GroupId == actorGroup.GroupId))
            {
                _workFlowActorGroups.Add(actorGroup);
            }
        }
    }

    public bool DeactiveRole(long roleId)
    {
        var result = _workFlowActorRoles.Where(x => x.Id == new WorkFlowActorRoleId(roleId)).FirstOrDefault();
        if (result is not null)
        {
            result.Deactive();
            return true;
        }
        else
            return false;

    }

    public bool DeactiveGroup(long groupId)
    {
        var result = _workFlowActorGroups.Where(x => x.Id == new WorkFlowActorGroupId(groupId)).FirstOrDefault();
        if (result is not null)
        {
            result.Deactive();
            return true;
        }
        else
            return false;

    }

    public bool DeactiveUser(long userId)
    {
        var result = _workFlowActorUsers.Where(x => x.Id == new WorkFlowActorUserId(userId)).FirstOrDefault();
        if (result is not null)
        {
            result.Deactive();
            return true;
        }
        else
            return false;

    }

    public WorkFlowActorId Id { get; set; }
    public WorkFlowId WorkFlowId { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long? ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
    public WorkFlow.Entities.WorkFlow WorkFlow { get; set; }

    private List<WorkFlowActorRole> _workFlowActorRoles = new();
    public IList<WorkFlowActorRole> WorkFlowActorRoles => _workFlowActorRoles;
    public virtual ICollection<WorkFlowActorStep> WorkFlowActorSteps { get; set; } = new List<WorkFlowActorStep>();
    private List<WorkFlowActorUser> _workFlowActorUsers = new();
    public ICollection<WorkFlowActorUser> WorkFlowActorUsers => _workFlowActorUsers;
    private List<WorkFlowActorGroup> _workFlowActorGroups = new();
    public ICollection<WorkFlowActorGroup> WorkFlowActorGroups => _workFlowActorGroups;

}
