namespace SIMA.Domain.Models.Features.DMS.DocumentTypes.Args;

public class CreateDocumentTypeArg
{
    public string? Name { get; set; }

    public string? Code { get; set; }

    public long ActiveStatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public long? CreatedBy { get; set; }
}
