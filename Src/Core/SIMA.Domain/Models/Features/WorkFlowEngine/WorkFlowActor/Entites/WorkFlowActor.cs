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
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        IsDirectManagerOfIssueCreator = arg.IsDirectManagerOfIssueCreator;
    }

    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
        #region DeleteRtelatedEntities
        foreach (var workflowActorStep in _workFlowActorSteps)
        {
            workflowActorStep.Delete();
        }
        #endregion
    }
    public void Activate()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Active;
        #region ActivateRtelatedEntities
        foreach (var workflowActorStep in _workFlowActorSteps)
        {
            workflowActorStep.Activate();
        }
        #endregion
    }

    public void AddActorRoles(List<CreateWorkFlowActorRoleArg> args , long workFlowActorId)
    {
        var previousRoleId = _workFlowActorRoles.Where(x=>x.WorkFlowActorId == new WorkFlowActorId(workFlowActorId)).ToList();

        foreach (var role in args)
        {
            if(_workFlowActorRoles.Any(x=>(x.WorkFlowActorId == new WorkFlowActorId(workFlowActorId) && x.RoleId ==new RoleId(role.RoleId)) && x.ActiveStatusId != (long)ActiveStatusEnum.Active))
            {
                var entity = _workFlowActorRoles.FirstOrDefault(x => x.WorkFlowActorId == new WorkFlowActorId(workFlowActorId) && x.RoleId == new RoleId(role.RoleId));
                entity.ActiveStatusId = (long)ActiveStatusEnum.Active;
            }
            else if(!_workFlowActorRoles.Any(x => (x.WorkFlowActorId == new WorkFlowActorId(workFlowActorId) && x.RoleId == new RoleId(role.RoleId)) && x.ActiveStatusId == (long)ActiveStatusEnum.Active))
            {
               var arg =  WorkFlowActorRole.New(role);
               _workFlowActorRoles.Add(arg);
            }
        }

        foreach (var role in previousRoleId)
        {
            var roles = args.FirstOrDefault(x => x.RoleId == role.RoleId.Value);
            if ( roles is null)
            {
                var entity = _workFlowActorRoles.FirstOrDefault(x =>x.RoleId == role.RoleId);
                entity.ActiveStatusId = (long)ActiveStatusEnum.Delete;
            }
        }
    }
    public void AddActorUsers(List<CreateWorkFlowActorUserArg> args , long workFlowActorId)
    {

        var previousUserId = _workFlowActorUsers.Where(x => x.WorkFlowActorId == new WorkFlowActorId(workFlowActorId)).ToList();

        foreach (var user in args)
        {
            if (_workFlowActorUsers.Any(x => (x.WorkFlowActorId == new WorkFlowActorId(workFlowActorId) && x.UserId == new UserId(user.UserId)) && x.ActiveStatusId != (long)ActiveStatusEnum.Active))
            {
                var entity = _workFlowActorUsers.FirstOrDefault(x => x.WorkFlowActorId == new WorkFlowActorId(workFlowActorId) && x.UserId == new UserId(user.UserId));
                entity.ActiveStatusId = (long)ActiveStatusEnum.Active;
            }
            else if (!_workFlowActorUsers.Any(x => (x.WorkFlowActorId == new WorkFlowActorId(workFlowActorId) && x.UserId == new UserId(user.UserId)) && x.ActiveStatusId == (long)ActiveStatusEnum.Active))
            {
                var arg = WorkFlowActorUser.New(user);
                _workFlowActorUsers.Add(arg);
            }
        }

        foreach (var role in previousUserId)
        {
            var users = args.FirstOrDefault(x => x.UserId == role.UserId.Value);
            if (users is null)
            {
                var entity = _workFlowActorUsers.FirstOrDefault(x => x.UserId == role.UserId);
                entity.ActiveStatusId = (long)ActiveStatusEnum.Delete;
            }
        }
    }
    public void AddActorGroups(List<CreateWorkFlowActorGroupArg> args, long workFlowActorId)
    {


        var previousGroupId = _workFlowActorGroups.Where(x => x.WorkFlowActorId == new WorkFlowActorId(workFlowActorId)).ToList();

        foreach (var group in args)
        {
            if (_workFlowActorGroups.Any(x => (x.WorkFlowActorId == new WorkFlowActorId(workFlowActorId) && x.GroupId == new GroupId(group.GroupId)) && x.ActiveStatusId != (long)ActiveStatusEnum.Active))
            {
                var entity = _workFlowActorGroups.FirstOrDefault(x => x.WorkFlowActorId == new WorkFlowActorId(workFlowActorId) && x.GroupId == new GroupId(group.GroupId));
                entity.ActiveStatusId = (long)ActiveStatusEnum.Active;
            }
            else if (!_workFlowActorGroups.Any(x => (x.WorkFlowActorId == new WorkFlowActorId(workFlowActorId) && x.GroupId == new GroupId(group.GroupId)) && x.ActiveStatusId == (long)ActiveStatusEnum.Active))
            {
                var arg = WorkFlowActorGroup.New(group);
                _workFlowActorGroups.Add(arg);
            }
        }

        foreach (var role in previousGroupId)
        {
            var groups = args.FirstOrDefault(x => x.GroupId == role.GroupId.Value);
            if (groups is null)
            {
                var entity = _workFlowActorGroups.FirstOrDefault(x => x.GroupId == role.GroupId);
                entity.ActiveStatusId = (long)ActiveStatusEnum.Delete;
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
    public string? IsDirectManagerOfIssueCreator { get; private set; }
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
