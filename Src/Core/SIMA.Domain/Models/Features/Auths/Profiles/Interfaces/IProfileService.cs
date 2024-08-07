using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.Auths.Profiles.Interfaces;

public interface IProfileService : IDomainService
{
    bool IsValidNationalCode(string nationalCode);
    Task<bool> IsNationalCodeUnique(string nationalCode);
}
