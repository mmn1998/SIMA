using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.LicenseStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.LicenseStatuses;

public interface ILicenseStatusQueryRepository : IQueryRepository
{
    Task<GetLicenseStatusQueryResult> GetById(GetLicenseStatusQuery request);
    Task<Result<IEnumerable<GetLicenseStatusQueryResult>>> GetAll(GetAllLicenseStatusesQuery request);
}