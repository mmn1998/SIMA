using SIMA.Application.Query.Contract.Features.TrustyDrafts.ResponsibilityWageTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.ResponsibilityWageTypes;

public interface IResponsibilityWageTypeQueryRepository : IQueryRepository
{
    Task<GetResponsibilityWageTypeQueryResult> GetById(GetResponsibilityWageTypeQuery request);
    Task<Result<IEnumerable<GetResponsibilityWageTypeQueryResult>>> GetAll(GetAllResponsibilityWageTypesQuery request);
}