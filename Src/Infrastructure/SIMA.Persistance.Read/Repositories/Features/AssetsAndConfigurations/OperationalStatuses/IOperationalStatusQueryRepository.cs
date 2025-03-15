using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.LicenseTypes;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.OperationalStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.OperationalStatuses;

public interface IOperationalStatusQueryRepository: IQueryRepository
{
    Task<GetOperationalStatusQueryResult> GetById(GetOperationalStatusQuery request);
    Task<Result<IEnumerable<GetOperationalStatusQueryResult>>> GetAll(GetAllOperationalStatusQuery request);
}