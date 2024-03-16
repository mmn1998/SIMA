using SIMA.Domain.Models.Features.Auths.Groups.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Roles.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Domain.Models.Features.IssueManagement.IssueApprovals.Entities;
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
    private WorkFlowActor(WorkFlowActorArg arg)
    {
        Id = new WorkFlowActorId(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        BpmnId = arg.BpmnId;
        CreatedBy = arg.UserId;
        CreatedAt = arg.CreatedAt;
        WorkFlowId = new WorkFlowId(arg.WorkFlowId);
        ActiveStatusId = arg.ActiveStatusId;
    }
    public static WorkFlowActor New(WorkFlowActorArg arg)
    {
        var result = new WorkFlowActor(arg);
        return result;
    }

    public void Modify(WorkFlowActorArg arg)
    {
        Code = arg.Code;
        Name = arg.Name;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedBy = arg.UserId;
    }
    public void Modify(ModifyWorkFlowActorArg arg)
    {
        Code = arg.Code;
        Name = arg.Name;
        WorkFlowId = new WorkFlowId(arg.WorkFlowId);
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }

    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }

    public void AddActorRoles(List<CreateWorkFlowActorRoleArg> args)
    {
        foreach (var arg in args)
        {
            if (!_workFlowActorRoles.Any(war => war.WorkFlowActorId == Id && war.RoleId == new RoleId(arg.RoleId)))
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
            if (!_workFlowActorUsers.Any(wau => wau.WorkFlowActorId == Id && wau.UserId == new UserId(arg.UserId)))
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
            if (!_workFlowActorGroups.Any(wag => wag.WorkFlowActorId == Id && wag.GroupId == new GroupId(arg.GroupId)))
            {
                arg.WorkFlowActorId = Id;
                var actorGroup = WorkFlowActorGroup.New(arg);
                _workFlowActorGroups.Add(actorGroup);
            }
        }
    }

    public bool DeleteRole(long roleId)
    {
        var result = _workFlowActorRoles.Where(x => x.Id == new WorkFlowActorRoleId(roleId)).FirstOrDefault();
        if (result is not null)
        {
            result.Delete();
            return true;
        }
        else
            return false;

    }

    public bool DeleteGroup(long groupId)
    {
        var result = _workFlowActorGroups.Where(x => x.Id == new WorkFlowActorGroupId(groupId)).FirstOrDefault();
        if (result is not null)
        {
            result.Delete();
            return true;
        }
        else
            return false;

    }

    public bool DeleteUser(long userId)
    {
        var result = _workFlowActorUsers.Where(x => x.Id == new WorkFlowActorUserId(userId)).FirstOrDefault();
        if (result is not null)
        {
            result.Delete();
            return true;
        }
        else
            return false;

    }

    public WorkFlowActorId Id { get; private set; }
    public WorkFlowId WorkFlowId { get; private set; }
    public string? Name { get; private set; }
    public string? Code { get; private set; }
    public long? ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public string BpmnId { get; private set; }
    public long? ModifiedBy { get; set; }
    public WorkFlow.Entities.WorkFlow WorkFlow { get; private set; }

    private List<WorkFlowActorRole> _workFlowActorRoles = new();
    public IList<WorkFlowActorRole> WorkFlowActorRoles => _workFlowActorRoles;
    private List<WorkFlowActorStep> _workFlowActorSteps = new();
    public virtual ICollection<WorkFlowActorStep> WorkFlowActorSteps => _workFlowActorSteps;
    private List<WorkFlowActorUser> _workFlowActorUsers = new();
    public ICollection<WorkFlowActorUser> WorkFlowActorUsers => _workFlowActorUsers;
    private List<WorkFlowActorGroup> _workFlowActorGroups = new();
    public ICollection<WorkFlowActorGroup> WorkFlowActorGroups => _workFlowActorGroups;
    private List<IssueApproval> _issueApprovals = new();
    public ICollection<IssueApproval> IssueApprovals => _issueApprovals;

}
