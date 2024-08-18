using SIMA.Domain.Models.Features.ServiceCatalogs.ApiAuthenticationMethods.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiAuthenticationMethods.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.ApiAuthenticationMethods.Contracts
{
    public interface IApiAuthenticationMethodRepository : IRepository<ApiAuthenticationMethod>
    {
        Task<ApiAuthenticationMethod> GetById(ApiAuthenticationMethodId id);
    }
}
