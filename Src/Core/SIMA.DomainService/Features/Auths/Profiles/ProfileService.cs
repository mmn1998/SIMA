using SIMA.Domain.Models.Features.Auths.Profiles.Interfaces;
using SIMA.Framework.Common.Helper;

namespace SIMA.DomainService.Features.Auths.Profiles;

public class ProfileService : IProfileService
{
    public bool IsValidNationalCode(string nationalCode)
    {
        return nationalCode.IsNationalID();
    }
}
