using SIMA.Domain.Models.Features.BranchManagement.PaymentTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.PaymentTypes.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.PaymentCommands.Entities;
using SIMA.Domain.Models.Features.Logistics.PaymentCommands.ValueObjects;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Entities;

public class PaymentHistory : Entity
{    
    public PaymentHistoryId Id { get; private set; }
    public DateTime PaymentDate { get; private set; }
    public LogisticsRequestId LogisticsRequestId { get; private set; }
    public virtual LogisticsRequest LogisticsRequest { get; private set; }
    public LogisticsRequestDocumentId PaymentDocumentId { get; private set; }
    public virtual LogisticsRequestDocument PaymentDocument { get; private set; }
    public PaymentCommandId PaymentCommandId { get; private set; }
    public virtual PaymentCommand PaymentCommand { get; private set; }
    public PaymentTypeId PaymentTypeId { get; private set; }
    public virtual PaymentType PaymentType { get; private set; }
    public string? PaymentNumber { get; private set; }
    public string? IsPrePayment { get; private set; }
    public string? Description { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}