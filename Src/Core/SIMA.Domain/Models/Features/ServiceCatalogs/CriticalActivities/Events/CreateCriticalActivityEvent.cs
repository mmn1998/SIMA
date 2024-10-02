using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Events;

public record CreateCriticalActivityEvent(long issueId, MainAggregateEnums mainAggregateType, string name, long sourceId) : IDomainEvent;