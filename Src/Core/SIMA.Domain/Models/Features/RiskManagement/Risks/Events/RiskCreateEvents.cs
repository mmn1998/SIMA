using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.RiskManagement.Risks.Events;

public sealed record RiskCreateEvents(long issueId, MainAggregateEnums MainAggregateType, string Name, long SourceId) : IDomainEvent;