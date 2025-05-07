using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.ValueObjects;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Entities;

public class TenderResult : Entity
{
    public TenderResultId Id { get; private set; }
    public DateTime? TenderDate { get; private set; }
    public LogisticsSupplyId LogisticsSupplyId { get; private set; }
    public virtual LogisticsSupply LogisticsSupply { get; private set; }
    public LogisticsSupplyDocumentId? TenderDocumentId { get; private set; }
    public virtual LogisticsSupplyDocument? TenderDocument { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}