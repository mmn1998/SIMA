namespace SIMA.Domain.Models.Features.DMS.Documents.Args;

public class CreateDocumentArg
{
    public string? Name { get; set; }
    public string? IsOrganizationalDocumentation { get; set; }
    public string? Code { get; set; }
    public long? MainAggregateId { get; set; }
    public long? SourceId { get; set; }
    public long CompanyId { get; set; }
    public long? AttachStepId { get; set; }
    public long DocumentTypeId { get; set; }
    public long FileExtensionId { get; set; }
    public string? FileAddress { get; set; }
    public long ActiveStatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public long? CreatedBy { get; set; }
}
