using SIMA.Application.Query.Contract.Features.Auths.OwnershipTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.OwnershipTypes;

public interface IOwnershipTypeQueryRepository : IQueryRepository
{
    Task<GetOwnershipTypeQueryResult> GetById(GetOwnershipTypeQuery request);
    Task<Result<IEnumerable<GetOwnershipTypeQueryResult>>> GetAll(GetAllOwnershipTypesQuery request);
}
