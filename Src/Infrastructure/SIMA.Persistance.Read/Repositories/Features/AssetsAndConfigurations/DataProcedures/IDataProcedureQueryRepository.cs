using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.DataProcedures;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.DataProcedures;

public interface IDataProcedureQueryRepository : IQueryRepository
{
    Task<GetDataProcedureQueryResult> GetById(GetDataProcedureQuery request);
    Task<Result<IEnumerable<GetDataProcedureQueryResult>>> GetAll(GetAllDataProceduresQuery request);
}