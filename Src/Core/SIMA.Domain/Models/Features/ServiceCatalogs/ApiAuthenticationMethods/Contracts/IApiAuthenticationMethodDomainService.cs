using SIMA.Domain.Models.Features.ServiceCatalogs.ApiAuthenticationMethods.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.ApiAuthenticationMethods.Contracts;

public interface IApiAuthenticationMethodDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, ApiAuthenticationMethodId? id = null);
}
