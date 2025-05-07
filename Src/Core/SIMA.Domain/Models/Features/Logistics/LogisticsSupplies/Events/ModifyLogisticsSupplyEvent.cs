using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Events;

public sealed record ModifyLogisticsSupplyEvent(long issueId, MainAggregateEnums MainAggregate, long issuePriority,
    DateTime dueDate, int weight, string summery, long? RequesterId) : IDomainEvent;
