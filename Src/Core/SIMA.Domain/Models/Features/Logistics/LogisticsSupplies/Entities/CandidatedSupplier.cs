using SIMA.Domain.Models.Features.Auths.Suppliers.Entities;
using SIMA.Domain.Models.Features.Auths.Suppliers.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.Orderings.Entities;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Entities;

public class CandidatedSupplier : Entity
{
    public CandidatedSupplierId Id { get; private set; }
    public LogisticsSupplyId LogisticsSupplyId { get; private set; }
    public virtual LogisticsSupply LogisticsSupply { get; private set; }
    public SupplierId SupplierId { get; private set; }
    public virtual Supplier Supplier { get; private set; }
    public string? IsSelected { get; private set; }
    public DateTime? SelectionDate { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    private List<RequestInquiry> _requestInquiries = new();
    public ICollection<RequestInquiry> RequestInquiries => _requestInquiries;
    private List<Ordering> _orderings = new();
    public ICollection<Ordering> Orderings => _orderings;

    private List<SupplierContract> _supplierContracts = new();
    public ICollection<SupplierContract> SupplierContracts => _supplierContracts;

}
