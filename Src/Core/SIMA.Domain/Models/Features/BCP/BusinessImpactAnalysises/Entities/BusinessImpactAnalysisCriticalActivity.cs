using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Args;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Entities;

public class BusinessImpactAnalysisCriticalActivity : Entity
{
    private BusinessImpactAnalysisCriticalActivity() { }
    private BusinessImpactAnalysisCriticalActivity(CreateBusinessImpactAnalysisCriticalActivityArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        BusinessImpactAnalysisId = new(arg.BusinessImpactAnalysisId);
        CriticalActivityId = new(arg.CriticalActivitytId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static BusinessImpactAnalysisCriticalActivity Create(CreateBusinessImpactAnalysisCriticalActivityArg arg)
    {
        return new BusinessImpactAnalysisCriticalActivity(arg);
    }
    public void Modify(ModifyBusinessImpactAnalysisCriticalActivityArg arg)
    {
        BusinessImpactAnalysisId = new(arg.BusinessImpactAnalysisId);
        CriticalActivityId = new(arg.CriticalActivityId);
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public BusinessImpactAnalysisCriticalActivityId Id { get; set; }
    public BusinessImpactAnalysisId BusinessImpactAnalysisId { get; private set; }
    public virtual BusinessImpactAnalysis BusinessImpactAnalysis { get; private set; }
    
    public CriticalActivityId CriticalActivityId { get; private set; }
    public virtual CriticalActivity CriticalActivity { get; private set; }
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