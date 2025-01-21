using SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseTypes.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseTypes.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseTypes.Contracts;

public interface ILicenseTypeRepository : IRepository<LicenseType>
{
    Task<LicenseType> GetById(LicenseTypeId id);
}