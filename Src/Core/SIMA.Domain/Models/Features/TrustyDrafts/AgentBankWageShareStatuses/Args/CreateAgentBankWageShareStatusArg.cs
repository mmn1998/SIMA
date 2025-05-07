namespace SIMA.Domain.Models.Features.TrustyDrafts.AgentBankWageShareStatuses.Args;

public class CreateAgentBankWageShareStatusArg
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}
