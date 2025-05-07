using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.ValueObjects;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Entities;

public class RequestInquiry : Entity
{
    public RequestInquiryId Id { get; private set; }
    public LogisticsSupplyDocumentId? InvoiceDocumentId { get; private set; }
    public virtual LogisticsSupplyDocument? InvoiceDocument { get; private set; }
    public CandidatedSupplierId? CandidatedSupplierId { get; private set; }
    public virtual CandidatedSupplier? CandidatedSupplier { get; private set; }
    public double InquieredPrice { get; private set; }
    public string? IsWrittenInquiry { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}
