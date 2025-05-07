using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.Auths.Groups.Event;

public sealed record FromGroupPermissionEvent(List<long> FormIds) : IDomainEvent;
