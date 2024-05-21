using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SIMA.Domain.Models.Features.Auths.Companies.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Profiles.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.Interfaces;
using SIMA.Framework.Common.Security;
using SIMA.Persistance.Persistence;
using SIMA.Persistance.Read.Repositories.Features.Auths.Users;
using System.Text.RegularExpressions;

namespace SIMA.DomainService.Features.Auths.Users;


public class UserService : IUserService
{
    private readonly IUserQueryRepository _repository;
    private readonly SIMADBContext _context;
    private readonly PasswordPolicy _passwordPolicy;
    private static readonly Regex _lowercaseRegex = new Regex("[a-z]");
    private static readonly Regex _uppercaseRegex = new Regex("[A-Z]");
    private static readonly Regex _digitRegex = new Regex("[0-9]");
    private static readonly Regex _specialCharRegex = new Regex("[!@#$%^&*(),.?\":{}|<>]");
    private static readonly Regex _minLengthRegex = new Regex(".{6,}");

    public UserService(IUserQueryRepository repository, SIMADBContext context, IOptions<PasswordPolicy> passwordPolicy)
    {
        _repository = repository;
        _context = context;
        _passwordPolicy = passwordPolicy.Value;
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

    public bool IsPasswordSatisfied(string password)
    {
        int validationFailedCount = 0;

        if (string.IsNullOrEmpty(password)) validationFailedCount++;
        if (_passwordPolicy.HasLowerCase)
            if (!PasswordHasLowerCase(password)) validationFailedCount++;
        if (_passwordPolicy.HasUpperCase)
            if (!PasswordHasUpperCase(password)) validationFailedCount++;
        if (_passwordPolicy.HasSpecialCharacters)
            if (!PasswordHasSpecialCharacters(password)) validationFailedCount++;
        if (_passwordPolicy.HasDigits)
            if (!PasswordHasDigits(password)) validationFailedCount++;

        if (!PasswordMinLengthCheck(password)) validationFailedCount++;

        return validationFailedCount == 0;
    }
    #region PasswordChecks
    private bool PasswordHasLowerCase(string password)
    {
        return _lowercaseRegex.IsMatch(password);
    }
    private bool PasswordHasUpperCase(string password)
    {
        return _uppercaseRegex.IsMatch(password);
    }

    private bool PasswordMinLengthCheck(string password)
    {
        return _minLengthRegex.IsMatch(password);
    }
    private bool PasswordHasDigits(string password)
    {
        return _digitRegex.IsMatch(password);
    }
    private bool PasswordHasSpecialCharacters(string password)
    {
        return _specialCharRegex.IsMatch(password);
    }
    #endregion
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
