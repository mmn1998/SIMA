namespace SIMA.Application.Query.Contract.Features.DMS.Documents;

public class GetDownloadDocumentQueryResult
{
    public byte[] FileContent { get; set; } = new byte[0];
    public string Extension { get; set; } = "";
    public string? ContentType { get; set; } = "";
    public string? Name { get; set; } = "";
}
