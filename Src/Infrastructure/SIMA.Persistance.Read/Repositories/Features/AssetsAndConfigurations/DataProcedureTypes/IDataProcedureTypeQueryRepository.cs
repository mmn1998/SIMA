using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.DataProcedureTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.DataProcedureTypes;

public interface IDataProcedureTypeQueryRepository : IQueryRepository
{
    Task<GetDataProcedureTypeQueryResult> GetById(GetDataProcedureTypeQuery request);
    Task<Result<IEnumerable<GetDataProcedureTypeQueryResult>>> GetAll(GetAllDataProcedureTypesQuery request);
}