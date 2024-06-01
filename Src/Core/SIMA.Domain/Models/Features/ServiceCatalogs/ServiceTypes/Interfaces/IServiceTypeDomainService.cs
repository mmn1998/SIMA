using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.ServiceTypes.Interfaces;

public interface IServiceTypeDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, ServiceTypeId? Id = null);
}