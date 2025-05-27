using SIMA.Application.Query.Contract.Features.Auths.CustomeFieldTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.CustomeFieldTypes
{
    public interface ICustomeFieldTypeQueryRepository : IQueryRepository
    {
        Task<Result<IEnumerable<GetCustomeFieldTypeQueryResult>>> GetAll(GetAllCustomeFieldTypesQuery request);
    }
}
