using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.LicenseTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.LicenseTypes;

public interface ILicenseTypeQueryRepository : IQueryRepository
{
    Task<GetLicenseTypeQueryResult> GetById(GetLicenseTypeQuery request);
    Task<Result<IEnumerable<GetLicenseTypeQueryResult>>> GetAll(GetAllLicenseTypeQuery request);
}