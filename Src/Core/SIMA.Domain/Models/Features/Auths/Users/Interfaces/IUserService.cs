using SIMA.Domain.Models.Features.Auths.Companies.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Profiles.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.Auths.Users.Interfaces;

public interface IUserService : IDomainService
{
    bool IsPasswordSatisfied(string password);
    Task<bool> IsUsernameUnique(string username , long userId);
    Task<bool> IsUsrConfigSatisfied(long configurationId, long userId);
    bool IsUsernameValidRegex(string username);
    Task<bool> IsCompanyMatchPersonCompany(CompanyId companyId, ProfileId profileId);
    string GenerateCode();
    string GeneratePassword();
}

