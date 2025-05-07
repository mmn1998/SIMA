using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Args;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Contracts;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Entities;

public class BusinessContinuityStrategyObjective : Entity
{
    private BusinessContinuityStrategyObjective() { }
    private BusinessContinuityStrategyObjective(CreateBusinessContinuityStrategyObjectiveArg arg)
    {
        Id = new(arg.Id);
        BusinessContinuityStategyId = new(arg.BusinessContinuityStategyId);
        Title = arg.Title;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<BusinessContinuityStrategyObjective> Create(CreateBusinessContinuityStrategyObjectiveArg arg, IBusinessContinuityStategyDomainService service)
    {
        await CreateGuards(arg, service);
        return new BusinessContinuityStrategyObjective(arg);
    }
    public async Task Modify(ModifyBusinessContinuityStrategyObjectiveArg arg, IBusinessContinuityStategyDomainService service)
    {
        await ModifyGuards(arg, service);
        BusinessContinuityStategyId = new(arg.BusinessContinuityStategyId);
        Title = arg.Title;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateBusinessContinuityStrategyObjectiveArg arg, IBusinessContinuityStategyDomainService service)
    {
        arg.NullCheck();
        arg.Title.NullCheck();
        arg.Code.NullCheck();

        if (arg.Title.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsObjectiveCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyBusinessContinuityStrategyObjectiveArg arg, IBusinessContinuityStategyDomainService service)
    {
        arg.NullCheck();
        arg.Title.NullCheck();
        arg.Code.NullCheck();

        if (arg.Title.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsObjectiveCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    public BusinessContinuityStrategyObjectiveId Id { get; set; }
    public BusinessContinuityStrategyId BusinessContinuityStategyId { get; private set; }
    public virtual BusinessContinuityStrategy BusinessContinuityStategy { get; private set; }
    public string Code { get; private set; }
    public string Title { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    public void Active(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Active;
    }
}