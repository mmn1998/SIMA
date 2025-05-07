using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.Profiles.Interfaces;
using SIMA.Framework.Common.Helper;
using SIMA.Persistance.Persistence;
using System.Text.RegularExpressions;

namespace SIMA.DomainService.Features.Auths.Profiles;

public class ProfileService : IProfileService
{
    private readonly SIMADBContext _context;

    public ProfileService(SIMADBContext context)
    {
        _context = context;
    }

    public async Task<bool> IsNationalCodeUnique(string nationalCode)
    {
        return !await _context.Profiles.AnyAsync(i => i.NationalId == nationalCode);
    }

    public bool IsValidNationalCode(string nationalCode)
    {
        return nationalCode.IsNationalID();
    }
}
