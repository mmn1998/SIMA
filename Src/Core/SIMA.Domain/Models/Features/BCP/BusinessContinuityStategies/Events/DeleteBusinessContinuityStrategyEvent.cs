using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Events;

public sealed record DeleteBusinessContinuityStrategyEvent(long issueId) : IDomainEvent;
