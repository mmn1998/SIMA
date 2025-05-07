using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanStratgies.Entities;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Args;
using SIMA.Domain.Models.Features.RiskManagement.Risks.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.RiskManagement.Risks.Entities;

public class RiskValueStrategy : Entity
{
    public RiskValueStrategy()
    {

    }
    public RiskValueStrategy(CreateRiskValueStrategyArg arg)
    {
        Id = new(arg.Id);
        RiskId = new(arg.RiskId);
        StrategyId = new(arg.StrategyId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static RiskValueStrategy Create(CreateRiskValueStrategyArg arg)
    {
        return new RiskValueStrategy(arg);
    }
    public RiskValueStrategyId Id { get; private set; }
    public RiskId RiskId { get; private set; }
    public virtual Risk Risk { get; private set; }
    public BusinessContinuityPlanStratgyId StrategyId { get; private set; }
    public virtual BusinessContinuityPlanStratgy Strategy { get; private set; }
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
