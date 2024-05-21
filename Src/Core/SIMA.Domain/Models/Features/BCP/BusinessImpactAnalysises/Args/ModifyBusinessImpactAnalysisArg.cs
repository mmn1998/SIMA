namespace SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Args;

public class ModifyBusinessImpactAnalysisArg
{
    public long Id { get; set; }
    public string? Name { get; set; }

    public string? Code { get; set; }

    public long ActiveStatusId { get; set; }

    public byte[]? ModifiedAt { get; set; }

    public long? ModifiedBy { get; set; }
    public long ImportanceDegreeId { get; set; }
    public long ServicePriorityId { get; set; }
    public long BackupPeriodId { get; set; }
    public long? MaximumAcceptableOutageId { get; set; }
    public long? ConsequenceId { get; set; }

    /// TODO : ServiceId
    //public long ServiceId { get; set; }
    public float? RTO { get; set; }
    public float? RPO { get; set; }
    public float? WRT { get; set; }
    public float? MTD { get; set; }
    public string? RestartReason { get; set; }
}
