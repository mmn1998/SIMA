using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.ServiceBoundles.Interfaces;

public interface IServiceBoundleDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, ServiceBoundleId? Id = null);
}
