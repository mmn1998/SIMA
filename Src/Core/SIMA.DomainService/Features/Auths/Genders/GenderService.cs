using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.Genders.Interfaces;
using SIMA.Domain.Models.Features.Auths.Genders.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.Auths.Genders;

public class GenderService : IGenderService
{
    private readonly SIMADBContext _context;

    public GenderService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, long id)
    {
        if (id > 0)
            return !await _context.Genders.AnyAsync(b => b.Code == code && b.Id != new GenderId(id));
        else
            return !await _context.Genders.AnyAsync(b => b.Code == code);
    }
}
