using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Events;

public sealed record DeleteLogisticsSupplytEvent(long issueId) : IDomainEvent;
