using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.Suppliers.ValueObjects;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.Logistics.Suppliers.Entities;

public class CandidatedSupplier : Entity
{
    public CandidatedSupplierId Id { get; private set; }
    public LogisticsRequestId LogisticsRequestId { get; private set; }
    public virtual LogisticsRequest LogisticsRequest { get; private set; }
    public SupplierId SupplierId { get; private set; }
    public virtual Supplier Supplier { get; private set; }
    public string? IsSelected { get; private set; }
    public DateTime? SelectionDate { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}
