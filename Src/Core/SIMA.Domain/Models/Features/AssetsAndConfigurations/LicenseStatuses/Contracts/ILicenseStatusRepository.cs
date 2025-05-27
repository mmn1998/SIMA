using SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseStatuses.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseStatuses.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseStatuses.Contracts;

public interface ILicenseStatusRepository : IRepository<LicenseStatus>
{
    Task<LicenseStatus> GetById(LicenseStatusId id);
}