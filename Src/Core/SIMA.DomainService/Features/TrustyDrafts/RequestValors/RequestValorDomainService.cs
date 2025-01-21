using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.TrustyDrafts.RequestValors.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.RequestValors.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.TrustyDrafts.RequestValors;

public class RequestValorDomainService : IRequestValorDomainService
{
    private readonly SIMADBContext _context;

    public RequestValorDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, RequestValorId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.RequestValors.AnyAsync(x => x.Code == code);
        else result = !await _context.RequestValors.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}