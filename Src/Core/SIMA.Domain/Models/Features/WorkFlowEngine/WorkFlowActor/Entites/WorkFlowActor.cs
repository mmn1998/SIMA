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

    public void AddActorRoles(List<CreateWorkFlowActorRoleArg> args)
    {
        foreach (var arg in args)
        {
            if (!_workFlowActorRoles.Any(war => war.WorkFlowActorId == Id && war.RoleId == arg.RoleId))
            {
                arg.WorkFlowActorId = Id;
                var actorRole = WorkFlowActorRole.New(arg);
                _workFlowActorRoles.Add(actorRole);
            }
        }
    }
    public void AddActorUsers(List<CreateWorkFlowActorUserArg> args)
    {
        foreach (var arg in args)
        {
            if (!_workFlowActorUsers.Any(wau => wau.WorkFlowActorId == Id && wau.UserId == arg.UserId))
            {
                arg.WorkFlowActorId = Id;
                var actorUser = WorkFlowActorUser.New(arg);
                _workFlowActorUsers.Add(actorUser);
            }
        }
    }
    public void AddActorGroups(List<CreateWorkFlowActorGroupArg> args)
    {
        foreach (var arg in args)
        {
            if (!_workFlowActorGroups.Any(wag => wag.WorkFlowActorId == Id && wag.GroupId == arg.GroupId))
            {
                arg.WorkFlowActorId = Id;
                var actorGroup = WorkFlowActorGroup.New(arg);
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
