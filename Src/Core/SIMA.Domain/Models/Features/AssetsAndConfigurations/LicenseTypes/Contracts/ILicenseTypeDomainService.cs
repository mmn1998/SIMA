using SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseTypes.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseTypes.Contracts;

public interface ILicenseTypeDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, LicenseTypeId? id = null);
}
