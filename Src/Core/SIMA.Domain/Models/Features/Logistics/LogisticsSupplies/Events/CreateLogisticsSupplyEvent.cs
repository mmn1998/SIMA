using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Events;

public sealed record CreateLogisticsSupplyEvent(long issueId, MainAggregateEnums MainAggregateType, string Name, long SourceId,
    long IssuePriority, DateTime? DueDate, long? RequesterId) : IDomainEvent;
