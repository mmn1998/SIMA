using SIMA.Application.Query.Contract.Features.SecurityCommitees.Cartables;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.SecurityCommitees.Cartables;

public interface ICartableQueryRepository:IQueryRepository
{
    Task<Result<IEnumerable<GetAllCartableQueryResult>>> GetAll(GetAllCartableQuery request);
    Task<GetCartableQueryResult> GetDetail(GetCartableQuery request);
}
