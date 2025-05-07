using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.RiskManagement.Risks.Events;

public sealed record DeleteRiskCreateEvents(long issueId) : IDomainEvent;
