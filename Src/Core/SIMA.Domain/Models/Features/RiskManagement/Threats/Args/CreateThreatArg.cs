namespace SIMA.Domain.Models.Features.RiskManagement.Threats.Args;

public class CreateThreatArg
{
    public long Id { get; set; }
    public string Description { get;  set; }
    public long RiskId { get;  set; }
    public long RiskPossibilityId { get;  set; }
    public long ThreatTypeId { get;  set; }
    public long ActiveStatusId { get;  set; }
    public DateTime? CreatedAt { get;  set; }
    public long? CreatedBy { get;  set; }
}
