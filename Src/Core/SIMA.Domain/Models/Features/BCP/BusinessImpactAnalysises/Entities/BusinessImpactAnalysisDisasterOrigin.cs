using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Args;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.ValueObjects;
using SIMA.Domain.Models.Features.BCP.Consequences.Entities;
using SIMA.Domain.Models.Features.BCP.Consequences.ValueObjects;
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
        RecoveryPointObjectiveId = new(arg.RecoveryPointObjectivesId);
        OriginId = new(arg.OriginId);
        Description = arg.Description ?? "";
        RTO = arg.RTO;
        MTD = arg.MTD;
        WRT = arg.WRT;
        RPO = arg.RPO;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static BusinessImpactAnalysisDisasterOrigin Create(CreateBusinessImpactAnalysisDisasterOriginArg arg)
    {
        return new BusinessImpactAnalysisDisasterOrigin(arg);
    }
    //public void Modify(ModifyBusinessImpactAnalysisDisasterOriginArg arg)
    //{
    //    ActiveStatusId = arg.ActiveStatusId;
    //    ConsequenceId = new(arg.ConsequenceId);
    //    HappeningPossibilityId = new(arg.HappeningPossibilityId);
    //    BusinessImpactAnalysisId = new(arg.BusinessImpactAnalysisId);
    //    RecoveryPointObjectiveId = new(arg.RecoveryPointObjectiveId);
    //    OriginId = new(arg.OriginId);
    //    Description = arg.Description ?? "";
    //    RTO = arg.RTO;
    //    MTD = arg.MTD;
    //    WRT = arg.WRT;
    //    RPO = arg.RPO;
    //    ModifiedAt = arg.ModifiedAt;
    //    ModifiedBy = arg.ModifiedBy;
    //}
    public BusinessImpactAnalysisDisasterOriginId Id { get; private set; }
    public BusinessImpactAnalysisId BusinessImpactAnalysisId { get; private set; }
    public virtual BusinessImpactAnalysis BusinessImpactAnalysis { get; private set; }
    public ConsequenceId ConsequenceId { get; private set; }
    public virtual Consequence Consequence { get; private set; }
    public OriginId OriginId { get; private set; }
    public virtual Origin Origin { get; private set; }
    public RecoveryPointObjectiveId RecoveryPointObjectiveId { get; private set; }
    public virtual RecoveryPointObjective RecoveryPointObjective { get; private set; }
    public string Description { get; private set; }
    public float? RTO { get; private set; }
    public float? RPO { get; private set; }
    public float? WRT { get; private set; }
    public float? MTD { get; private set; }
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
