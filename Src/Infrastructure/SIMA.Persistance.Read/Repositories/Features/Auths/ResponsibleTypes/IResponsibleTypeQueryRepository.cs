using SIMA.Application.Query.Contract.Features.Auths.ResponsibleTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.ResponsibleTypes;

public interface IResponsibleTypeQueryRepository : IQueryRepository
{
    Task<GetResponsibleTypeQueryResult> GetById(GetResponsibleTypeQuery request);
    Task<Result<IEnumerable<GetResponsibleTypeQueryResult>>> GetAll(GetAllResponsibleTypeQuery request);
}