using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.AddressTypes.Interfaces;
using SIMA.Domain.Models.Features.Auths.AddressTypes.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.Auths.AddressTypes;

public class AddressTypeDomainService : IAddressTypeDomainService
{
    private readonly SIMADBContext _context;

    public AddressTypeDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, long id)
    {
        if (id > 0)
            return !await _context.AddressTypes.AnyAsync(b => b.Code == code && b.Id != new AddressTypeId(id));
        else
            return !await _context.AddressTypes.AnyAsync(b => b.Code == code);
    }
}
