namespace SIMA.Domain.Models.Features.DMS.DocumentExtensions.Args;

public class CreateDocumentExtensionArg
{
    public string? Name { get; set; }

    public string? Code { get; set; }

    public long ActiveStatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public long? CreatedBy { get; set; }
}
