using SIMA.Application.Query.Contract.Features.Auths.Gender;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.Genders;

public interface IGenderQueryRepository : IQueryRepository
{
    Task<GetGenderQueryResult> FindById(long id);
    Task<Result<IEnumerable<GetGenderQueryResult>>> GetAll(GetAllGenderQuery? baseRequests = null);
}
