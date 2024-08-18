using SIMA.Application.Query.Contract.Features.ServiceCatalog.ApiTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.ApiTypes
{
    public interface IApiTypeQueryRepository : IQueryRepository
    {
        Task<GetApiTypesQueryResult> GetById(GetApiTypeQuery request);
        Task<Result<IEnumerable<GetApiTypesQueryResult>>> GetAll(GetAllApiTypesQuery request);
    }
}
