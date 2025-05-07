using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanStratgies.Args;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanStratgies.Entities;

public class BusinessContinuityPlanStratgy : Entity
{
    private BusinessContinuityPlanStratgy()
    {

    }
    private BusinessContinuityPlanStratgy(CreateBusinessContinuityPlanStratgyArg arg)
    {
        Id = new BusinessContinuityPlanStratgyId(arg.Id);
        BusinessContinuityPlanId = new(arg.BusinessContinuityPlanId);
        BusinessContinuityStratgyId = new(arg.BusinessContinuityStratgyId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static BusinessContinuityPlanStratgy Create(CreateBusinessContinuityPlanStratgyArg arg)
    {
        return new BusinessContinuityPlanStratgy(arg);
    }

    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }

    public void ChangeStatus(ActiveStatusEnum status)
    {
        ActiveStatusId = (long)status;
    }
    public BusinessContinuityPlanStratgyId Id { get; set; }
    public BusinessContinuityStrategyId BusinessContinuityStratgyId { get; private set; }
    public virtual BusinessContinuityStrategy BusinessContinuityStrategy { get; private set; }
    public BusinessContinuityPlanId BusinessContinuityPlanId { get; private set; }
    public virtual BusinessContinuityPlan BusinessContinuityPlan { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    private List<RiskValueStrategy> _riskValueStrategies = new();
    public ICollection<RiskValueStrategy> RiskValueStrategies => _riskValueStrategies;
}
