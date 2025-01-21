using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Events;

public sealed record DeleteBusinessImpactAnalysisEvent(long issueId) : IDomainEvent;
