namespace SIMA.Application.Query.Contract.Features.DMS.DocumentTypes;

public class GetDocumentTypeQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? ActiveStatus { get; set; }
    public long? ActiveStatusId { get; set; }
}
