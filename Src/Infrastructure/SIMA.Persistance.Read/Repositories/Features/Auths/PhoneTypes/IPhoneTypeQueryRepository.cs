using SIMA.Application.Query.Contract.Features.Auths.PhoneTypes;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.PhoneTypes;

public interface IPhoneTypeQueryRepository : IQueryRepository
{
    Task<GetPhoneTypeQueryResult> FindById(long id);
    Task<Result<IEnumerable<GetPhoneTypeQueryResult>>> GetAll(GetAllPhoneTypesQuery? request = null);
}
