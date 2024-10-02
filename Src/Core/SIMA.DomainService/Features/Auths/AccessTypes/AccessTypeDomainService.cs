using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.AccessTypes.Contracts;
using SIMA.Domain.Models.Features.Auths.AccessTypes.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.Auths.AccessTypes;

public class AccessTypeDomainService : IAccessTypeDomainService
{
    private readonly SIMADBContext _context;

    public AccessTypeDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, AccessTypeId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.AccessTypes.AnyAsync(x => x.Code == code);
        else result = !await _context.AccessTypes.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}