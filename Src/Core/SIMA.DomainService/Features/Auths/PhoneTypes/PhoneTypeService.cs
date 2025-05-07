using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.PhoneTypes.Interfaces;
using SIMA.Domain.Models.Features.Auths.PhoneTypes.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.Auths.PhoneTypes;

public class PhoneTypeService : IPhoneTypeService
{
    private readonly SIMADBContext _context;

    public PhoneTypeService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, long id)
    {
        if (id > 0)
            return !await _context.PhoneTypes.AnyAsync(b => b.Code == code && b.Id != new PhoneTypeId(id));
        else
            return !await _context.PhoneTypes.AnyAsync(b => b.Code == code);
    }
}
