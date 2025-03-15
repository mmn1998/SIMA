namespace SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Args;

public class CreateApiDocumentArg
{
    public long Id { get;  set; }
    public long ApiId { get;  set; }
    public long DocumentId { get;  set; }
    public long ActiveStatusId { get;  set; }
    public DateTime CreatedAt { get;  set; }
    public long CreatedBy { get;  set; }
}