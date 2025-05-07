namespace SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Args;

public class CreateLogisticsRequestDocumentArg
{
    public long LogisticsRequestId { get;  set; }
    public long DocumentId { get;  set; }
    public long ActiveStatusId { get;  set; }
    public DateTime? CreatedAt { get;  set; }
    public long? CreatedBy { get;  set; }
}