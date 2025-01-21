using SIMA.Domain.Models.Features.Auths.Suppliers.Entities;
using SIMA.Domain.Models.Features.Auths.Suppliers.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.PaymentTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.PaymentTypes.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.Orderings.Entities;
using SIMA.Domain.Models.Features.Logistics.Orderings.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.PaymentCommands.Entities;
using SIMA.Domain.Models.Features.Logistics.PaymentCommands.ValueObjects;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Entities;

public class PaymentHistory : Entity
{
    public PaymentHistoryId Id { get; private set; }
    public DateTime PaymentDate { get; private set; }
    public OrderingId OrderingId { get; private set; }
    public virtual Ordering Ordering { get; private set; }
    public LogisticsSupplyDocumentId? PaymentDocumentId { get; private set; }
    public virtual LogisticsSupplyDocument? PaymentDocument { get; private set; }
    public PaymentCommandId PaymentCommandId { get; private set; }
    public virtual PaymentCommand PaymentCommand { get; private set; }
    public PaymentTypeId PaymentTypeId { get; private set; }
    public virtual PaymentType PaymentType { get; private set; }
    public SupplierAccountListId? SupplierAccountListId { get; private set; }
    public virtual SupplierAccountList? SupplierAccountList { get; private set; }
    public double? PaymentValue { get; private set; }
    public string? PaymentNumber { get; private set; }
    public string? IsPrePayment { get; private set; }
    public string? Description { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}