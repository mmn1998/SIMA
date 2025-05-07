namespace SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Args;

public class CreateBusinessImpactAnalysisDocumentArg
{
    public long Id { get; set; }
    public long DocumentId { get; set; }
    public long BusinessImpactAnalysisId { get; set; }
    public long ActiveStatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public long CreatedBy { get; set; }
}
