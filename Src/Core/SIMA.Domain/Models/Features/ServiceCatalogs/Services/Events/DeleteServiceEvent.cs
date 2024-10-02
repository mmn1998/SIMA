using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.Services.Events;

public sealed record DeleteServiceEvent(long issueId) : IDomainEvent;
