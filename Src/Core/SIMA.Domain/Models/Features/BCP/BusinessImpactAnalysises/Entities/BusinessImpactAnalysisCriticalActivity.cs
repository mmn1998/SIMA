using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Args;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Args;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Entities;

public class BusinessImpactAnalysisCriticalActivity : Entity
{
    private BusinessImpactAnalysisCriticalActivity() { }
    private BusinessImpactAnalysisCriticalActivity(CreateBusinessImpactAnalysisCriticalActivityArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        BusinessImpactAnalysisId = new(arg.BusinessImpactAnalysisId);
        //StaffId = new(arg.CriticalActivitytId);
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
        //CriticalActivityId = new(arg.StaffId);
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public BusinessImpactAnalysisCriticalActivityId Id { get; set; }
    public BusinessImpactAnalysisId BusinessImpactAnalysisId { get; private set; }
    public virtual BusinessImpactAnalysis BusinessImpactAnalysis { get; private set; }
    /// <summary>
    /// TODO : CriticalActivity
    /// </summary>
    //public long CriticalActivityId { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}