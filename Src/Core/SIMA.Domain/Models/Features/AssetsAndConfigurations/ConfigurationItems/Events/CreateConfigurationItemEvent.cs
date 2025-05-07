using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Events;

public sealed record CreateConfigurationItemEvent(long issueId, MainAggregateEnums mainAggregateType, string name, long sourceId, long issuePriorityId, long issueWeightCategoryId) : IDomainEvent;