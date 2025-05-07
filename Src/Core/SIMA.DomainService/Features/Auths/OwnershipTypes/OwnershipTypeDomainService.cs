using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.OwnershipTypes.Contracts;
using SIMA.Domain.Models.Features.Auths.OwnershipTypes.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.Auths.OwnershipTypes;

public class OwnershipTypeDomainService : IOwnershipTypeDomainService
{
    private readonly SIMADBContext _context;

    public OwnershipTypeDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, OwnershipTypeId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.OwnershipTypes.AnyAsync(x => x.Code == code);
        else result = !await _context.OwnershipTypes.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}