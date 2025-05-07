using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceTypes.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.ServiceTypes.Contracts;

public interface IServiceTypeDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, ServiceTypeId? id = null);
}