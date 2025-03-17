using SIMA.Domain.Models.Features.BCP.BiaValues.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Args;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.ValueObjects;
using SIMA.Domain.Models.Features.BCP.ConsequenceIntensions.Entities;
using SIMA.Domain.Models.Features.BCP.ConsequenceIntensions.ValueObjects;
using SIMA.Domain.Models.Features.BCP.Consequences.Entities;
using SIMA.Domain.Models.Features.BCP.Consequences.ValueObjects;
using SIMA.Domain.Models.Features.BCP.ConsequenceValues.Entities;
using SIMA.Domain.Models.Features.BCP.ConsequenceValues.ValueObjects;
using SIMA.Domain.Models.Features.BCP.HappeningPossiblities.Entities;
using SIMA.Domain.Models.Features.BCP.HappeningPossiblities.ValueObjects;
using SIMA.Domain.Models.Features.BCP.Origins.Entities;
using SIMA.Domain.Models.Features.BCP.Origins.ValueObjects;
using SIMA.Domain.Models.Features.BCP.RecoveryPointObjectives.Entities;
using SIMA.Domain.Models.Features.BCP.RecoveryPointObjectives.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Entities;

public class BusinessImpactAnalysisDisasterOrigin : Entity, IAggregateRoot
{
    private BusinessImpactAnalysisDisasterOrigin()
    {

    }
    private BusinessImpactAnalysisDisasterOrigin(CreateBusinessImpactAnalysisDisasterOriginArg arg)
    {
        Id = new(arg.Id);
        ActiveStatusId = arg.ActiveStatusId;
        ConsequenceId = new(arg.ConsequenceId);
        BusinessImpactAnalysisId = new(arg.BusinessImpactAnalysisId);
        ConsequenceIntensionId = new(arg.ConsequenceIntensionId);
        OriginId = new(arg.OriginId);
        
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static BusinessImpactAnalysisDisasterOrigin Create(CreateBusinessImpactAnalysisDisasterOriginArg arg)
    {
        return new BusinessImpactAnalysisDisasterOrigin(arg);
    }
    public BusinessImpactAnalysisDisasterOriginId Id { get; private set; }
    public BusinessImpactAnalysisId BusinessImpactAnalysisId { get; private set; }
    public virtual BusinessImpactAnalysis BusinessImpactAnalysis { get; private set; }
    public OriginId OriginId { get; private set; }
    public virtual Origin Origin { get; private set; }
    public ConsequenceId ConsequenceId { get; private set; }
    public virtual Consequence Consequence { get; private set; }
    
    public ConsequenceIntensionId ConsequenceIntensionId { get; private set; }
    public virtual ConsequenceIntension ConsequenceIntension { get; private set; }



    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long CreatedBy { get; private set; }
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
