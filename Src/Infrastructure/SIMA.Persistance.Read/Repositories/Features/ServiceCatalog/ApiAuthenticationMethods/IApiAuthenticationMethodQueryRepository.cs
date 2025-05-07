using SIMA.Application.Query.Contract.Features.ServiceCatalog.ApiAuthenticationMethods;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.ApiAuthenticationMethods
{
    public interface IApiAuthenticationMethodQueryRepository : IQueryRepository
    {
        Task<GetApiAuthenticationMethodsQueryResult> GetById(GetApiAuthenticationMethodQuery request);
        Task<Result<IEnumerable<GetApiAuthenticationMethodsQueryResult>>> GetAll(GetAllApiAuthenticationMethodsQuery request);
    }
}
