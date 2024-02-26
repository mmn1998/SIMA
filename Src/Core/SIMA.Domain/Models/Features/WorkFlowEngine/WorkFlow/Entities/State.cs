using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Create;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Modify;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Interface;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities;

public class State : Entity
{

    private State()
    {

    }
    private State(CreateStateArg arg)
    {
        Id = new StateId(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
        WorkFlowId = new WorkFlowId((long)arg.WorkFlowId);
        CreatedBy = arg.CreatedBy;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
    }
    public  void Modify(ModifyStateArgs arg, IWorkFlowDomainService service)
    {
         ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        WorkFlowId = new WorkFlowId((long)arg.WorkFlowId);
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public static async Task<State> New(CreateStateArg arg, IWorkFlowDomainService service)
    {
        await CreateGuards(arg, service);
        return new State(arg);
    }
    public void Deactive()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Deactive;

    }

    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;

    }

    public StateId Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public WorkFlowId? WorkFlowId { get; set; }
    public long? ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
    public virtual ICollection<Step> Steps { get; set; } = new List<Step>();
    public virtual WorkFlow? WorkFlow { get; set; }

    #region Gaurds

    private static async Task CreateGuards(CreateStateArg arg, IWorkFlowDomainService service)
    {
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ActiveStatusId.NullCheck();
        if (await service.SteteIsCodeUnique(arg.Code, 0)) throw SimaResultException.UniqueCodeError;
    }
    private async Task ModifyGuards(ModifyStateArgs arg, IWorkFlowDomainService service)
    {
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ActiveStatusId.NullCheck();
        if (await service.SteteIsCodeUnique(arg.Code, arg.Id)) throw SimaResultException.UniqueCodeError;
    }
    #endregion
}
