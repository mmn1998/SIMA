using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Events;

public sealed record CreateLogisticsRequestEvent(long issueId, MainAggregateEnums MainAggregateType, string Name, long SourceId,
    long IssuePriority, DateTime? DueDate, long? RequesterId) : IDomainEvent;