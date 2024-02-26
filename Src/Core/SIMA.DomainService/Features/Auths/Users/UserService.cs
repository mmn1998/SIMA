using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.Companies.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Profiles.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.Interfaces;
using SIMA.Persistance.Persistence;
using SIMA.Persistance.Read.Repositories.Features.Auths.Users;
using System.Text.RegularExpressions;

namespace SIMA.DomainService.Features.Auths.Users;


public class UserService : IUserService
{
    private readonly IUserQueryRepository _repository;
    private readonly SIMADBContext _context;

    public UserService(IUserQueryRepository repository, SIMADBContext context)
    {
        _repository = repository;
        _context = context;
    }

    public async Task<bool> IsCompanyMatchPersonCompany(CompanyId companyId, ProfileId profileId)
    {
        bool result = false;
        if (await _context.Companies.AnyAsync(c => c.Id == companyId) && await _context.Profiles.AnyAsync(p => p.Id == profileId))
        {
            result = await _repository.IsCompanyMatchPersonCompany(companyId.Value, profileId.Value);
        }
        return result;
    }


    public async Task<bool> IsUsernameUnique(string username)
    {
        return await _repository.IsUsernameUnique(username);
    }

    public bool IsUsernameValidRegex(string username)
    {
        string pattern = "^[a-zA-Z0-9]+$";
        return Regex.IsMatch(username, pattern);
    }

    public async Task<bool> IsUsrConfigSatisfied(long configurationId, long userId)
    {
        return await _repository.IsUsrConfigSatisfied(configurationId, userId);
    }
}
