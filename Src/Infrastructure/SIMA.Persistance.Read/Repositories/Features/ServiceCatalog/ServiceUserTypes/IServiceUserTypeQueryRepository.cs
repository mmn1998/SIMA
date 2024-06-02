using SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceUserTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.ServiceUserTypes;

public interface IServiceUserTypeQueryRepository : IQueryRepository
{
    Task<GetServiceUserTypeQueryResult> GetById(GetServiceUserTypeQuery request);
    Task<Result<IEnumerable<GetServiceUserTypeQueryResult>>> GetAll(GetAllServiceUserTypesQuery request);
}