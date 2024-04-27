using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.Project.Events
{
    // Mahmoud domain events
    public sealed record ProjectCreatedEvent(int id, string name) : IDomainEvent;
}
