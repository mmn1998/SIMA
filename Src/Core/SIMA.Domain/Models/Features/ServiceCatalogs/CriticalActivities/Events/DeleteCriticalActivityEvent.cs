using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Events;

public record DeleteCriticalActivityEvent(long issueId) : IDomainEvent;

