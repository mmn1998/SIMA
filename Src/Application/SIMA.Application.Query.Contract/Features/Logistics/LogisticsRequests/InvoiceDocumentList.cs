namespace SIMA.Application.Query.Contract.Features.Logistics.LogisticsRequests;

public class InvoiceDocumentList
{
    public long Id { get; set; }
    public long InvoiceDocumentId { get; set; }
    public string? InvoiceDocumentPath { get; set; }
    public string? DocumentTypeName { get; set; }
    public long DocumentTypeId { get; set; }
    public long FileExtensionId { get; set; }
    public string? DocumentContentType { get; set; }
    public string? DocumentExtensionName { get; set; }
    public string? AttachStepName { get; set; }
}



