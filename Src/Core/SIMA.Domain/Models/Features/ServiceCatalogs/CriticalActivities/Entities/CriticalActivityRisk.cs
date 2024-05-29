
using SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Args;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Entities;

public class CriticalActivityRisk : Entity
{
    private CriticalActivityRisk() { }
    private CriticalActivityRisk(CreateCriticalActivityRiskArg arg)
    {
        Id = new CriticalActivityRiskId(arg.Id);
        CriticalActivityId = new CriticalActivityId(arg.CriticalActivityId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<CriticalActivityRisk> Create(CreateCriticalActivityRiskArg arg)
    {
        return new CriticalActivityRisk(arg);
    }
    public CriticalActivityRiskId Id { get; private set; }
    public CriticalActivityId CriticalActivityId { get; private set; }
    public virtual CriticalActivity CriticalActivity { get; private set; }
    /// <summary>
    /// TODO : riskId
    /// </summary>
    public long? ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}
