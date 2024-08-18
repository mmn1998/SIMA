using SIMA.Domain.Models.Features.ServiceCatalogs.ApiTypes.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.ApiTypes.Contracts;

public interface IApiTypeDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, ApiTypeId? id = null);
}