using SIMA.Application.Query.Contract.Features.Auths.AddressTypes;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.AddressTypes;

public interface IAddressTypeQueryRepository : IQueryRepository
{
    Task<GetAddressTypeQueryResult> FindById(long id);
    Task<Result<List<GetAddressTypeQueryResult>>> GetAll(GetAllAddressTypesQuery baseRequest);
    Task<List<GetAddressTypeQueryResult>> GetAllForRedis();
}

