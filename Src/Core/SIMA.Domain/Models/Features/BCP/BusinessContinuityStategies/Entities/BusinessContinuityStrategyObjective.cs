using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Args;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Entities;

public class BusinessContinuityStrategyObjective : Entity
{
    private BusinessContinuityStrategyObjective() { }
    private BusinessContinuityStrategyObjective(CreateBusinessContinuityStrategyObjectiveArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        BusinessContinuityStategyId = new(arg.BusinessContinuityStategyId);
        Title = arg.Title;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static BusinessContinuityStrategyObjective Create(CreateBusinessContinuityStrategyObjectiveArg arg)
    {
        return new BusinessContinuityStrategyObjective(arg);
    }
    public void Modify(ModifyBusinessContinuityStrategyObjectiveArg arg)
    {
        BusinessContinuityStategyId = new(arg.BusinessContinuityStategyId);
        Title = arg.Title;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public BusinessContinuityStrategyObjectiveId Id { get; set; }
    public BusinessContinuityStrategyId BusinessContinuityStategyId { get; private set; }
    public virtual BusinessContinuityStrategy BusinessContinuityStategy { get; private set; }
    public string? Code { get; private set; }
    public string? Title { get; private set; }
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
}