namespace SIMA.Application.Query.Contract.Features.DMS.Documents;

public class GetDocumentResult
{
    public long Id { get; set; }
    public string FileAddress { get; set; } = "";
    public string Extension { get; set; } = "";
}
