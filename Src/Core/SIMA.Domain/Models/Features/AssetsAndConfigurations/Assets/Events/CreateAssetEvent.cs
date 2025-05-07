using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Events;

public sealed record CreateAssetEvent(long issueId, MainAggregateEnums mainAggregateType, string name, long sourceId, long issuePriorityId, long issueWeightCategoryId) : IDomainEvent;