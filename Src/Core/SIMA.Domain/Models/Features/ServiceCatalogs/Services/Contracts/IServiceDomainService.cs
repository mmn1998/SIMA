using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.Services.Contracts;

public interface IServiceDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, ServiceId? id = null);
}