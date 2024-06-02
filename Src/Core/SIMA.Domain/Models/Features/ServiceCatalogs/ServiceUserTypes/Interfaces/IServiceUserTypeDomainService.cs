using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.ServiceUserTypes.Interfaces;

public interface IServiceUserTypeDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, ServiceUserTypeId? Id = null);
}