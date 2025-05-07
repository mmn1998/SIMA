using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Events;

public sealed record DeleteConfigurationItemEvent(long issueId) : IDomainEvent;
