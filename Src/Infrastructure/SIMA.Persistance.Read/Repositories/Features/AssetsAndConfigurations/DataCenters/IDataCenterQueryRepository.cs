using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.DataCenters;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.DataCenters;

public interface IDataCenterQueryRepository : IQueryRepository
{
    Task<GetDataCenterQueryResult> GetById(GetDataCenterQuery request);
    Task<Result<IEnumerable<GetDataCenterQueryResult>>> GetAll(GetAllDataCentersQuery request);
}