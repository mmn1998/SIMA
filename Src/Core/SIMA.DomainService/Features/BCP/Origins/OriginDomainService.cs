using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BCP.Origins.Contracts;
using SIMA.Domain.Models.Features.BCP.Origins.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.BCP.Origins;

public class OriginDomainService : IOriginDomainService
{
    private readonly SIMADBContext _context;

    public OriginDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, OriginId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.Origins.AnyAsync(x => x.Code == code);
        else result = !await _context.Origins.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}