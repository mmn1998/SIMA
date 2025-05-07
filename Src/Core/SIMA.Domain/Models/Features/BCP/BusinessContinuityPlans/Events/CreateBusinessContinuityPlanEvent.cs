using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Events;

public sealed record CreateBusinessContinuityPlanEvent(long issueId, MainAggregateEnums MainAggregateType, string Name, long SourceId) : IDomainEvent;
