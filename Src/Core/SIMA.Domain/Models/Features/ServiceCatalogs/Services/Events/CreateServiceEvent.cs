using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.Services.Events;

public sealed record CreateServiceEvent(long issueId, MainAggregateEnums mainAggregateType, string name, long sourceId, long issuePriorityId, long issueWeightCategoryId) : IDomainEvent;