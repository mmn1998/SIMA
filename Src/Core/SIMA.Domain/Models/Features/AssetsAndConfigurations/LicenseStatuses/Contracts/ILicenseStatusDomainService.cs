using SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseStatuses.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseStatuses.Contracts;

public interface ILicenseStatusDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, LicenseStatusId? id = null);
}