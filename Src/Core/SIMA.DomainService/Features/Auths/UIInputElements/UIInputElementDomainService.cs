using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.UIInputElements.Contracts;
using SIMA.Domain.Models.Features.Auths.UIInputElements.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.Auths.UIInputElements;

public class UIInputElementDomainService : IUIInputElementDomainService
{
    private readonly SIMADBContext _context;

    public UIInputElementDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, UIInputElementId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.UIInputElements.AnyAsync(x => x.Code == code);
        else result = !await _context.UIInputElements.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}