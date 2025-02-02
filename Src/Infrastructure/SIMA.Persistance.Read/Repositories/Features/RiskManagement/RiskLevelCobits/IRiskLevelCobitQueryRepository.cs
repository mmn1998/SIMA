using SIMA.Application.Query.Contract.Features.RiskManagement.RiskLevelCobits;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.RiskLevelCobits;

public interface IRiskLevelCobitQueryRepository : IQueryRepository
{
    Task<GetRiskLevelCobitQueryResult> GetById(GetRiskLevelCobitQuery request);
    Task<Result<IEnumerable<GetRiskLevelCobitQueryResult>>> GetAll(GetAllRiskLevelCobitsQuery request);
}