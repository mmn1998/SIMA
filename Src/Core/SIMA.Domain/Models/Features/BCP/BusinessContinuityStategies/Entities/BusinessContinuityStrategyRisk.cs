using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Args;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Entities;

public class BusinessContinuityStrategyRisk : Entity
{
    private BusinessContinuityStrategyRisk() { }
    private BusinessContinuityStrategyRisk(CreateBusinessContinuityStrategyRiskArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        BusinessContinuityStategyId = new(arg.BusinessContinuityStategyId);
        //RiskId = new(arg.RiskId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static BusinessContinuityStrategyRisk Create(CreateBusinessContinuityStrategyRiskArg arg)
    {
        return new BusinessContinuityStrategyRisk(arg);
    }
    public void Modify(ModifyBusinessContinuityStrategyRiskArg arg)
    {
        BusinessContinuityStategyId = new(arg.BusinessContinuityStategyId);
        //RiskId = new(arg.RiskId);
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public BusinessContinuityStrategyRiskId Id { get; set; }
    public BusinessContinuityStrategyId BusinessContinuityStategyId { get; private set; }
    public virtual BusinessContinuityStrategy BusinessContinuityStategy { get; private set; }
    public RiskId RiskId { get; private set; }
    public virtual Risk Risk { get; private set; }
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
