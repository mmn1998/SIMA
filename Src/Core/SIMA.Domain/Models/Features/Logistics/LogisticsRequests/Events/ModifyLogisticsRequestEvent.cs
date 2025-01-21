using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Events;

public sealed record ModifyLogisticsRequestEvent(long issueId, MainAggregateEnums MainAggregate, long issuePriority,
    DateTime dueDate, int weight, string summery) : IDomainEvent;