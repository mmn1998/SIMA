using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Events;

public sealed record CreateBusinessImpactAnalysisEvent(long issueId, MainAggregateEnums mainAggregateType, string name, long sourceId, long issuePriorityId, long issueWeightCategoryId) : IDomainEvent;