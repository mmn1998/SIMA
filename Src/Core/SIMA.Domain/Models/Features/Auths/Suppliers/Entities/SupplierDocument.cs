using SIMA.Domain.Models.Features.Auths.Suppliers.ValueObjects;
using SIMA.Domain.Models.Features.DMS.Documents.Entities;
using SIMA.Domain.Models.Features.DMS.Documents.ValueObjects;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.Auths.Suppliers.Entities;

public class SupplierDocument : Entity
{
    public SupplierDocumentId Id { get; private set; }
    public SupplierId SupplierId { get; private set; }
    public virtual Supplier Supplier { get; private set; }
    public DocumentId DocumentId { get; private set; }
    public virtual Document Document { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}
