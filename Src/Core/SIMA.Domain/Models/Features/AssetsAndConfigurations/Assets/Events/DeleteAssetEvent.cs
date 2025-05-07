using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Events;

public sealed record DeleteAssetEvent(long issueId) : IDomainEvent;