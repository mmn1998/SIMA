using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.Positions.Interfaces;
using SIMA.Domain.Models.Features.Auths.ResponsibleTypes.Interfaces;
using SIMA.Domain.Models.Features.Auths.ResponsibleTypes.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.Auths.ResponsibleTypes;

public class ResponsibleTypeService : IResponsibleTypeDomainService
{
    private readonly SIMADBContext _context;

    public ResponsibleTypeService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, long id)
    {
        if (id > 0)
            return !await _context.ResponsibleTypes.AnyAsync(b => b.Code == code && b.Id != new ResponsibleTypeId(id));
        else
            return !await _context.ResponsibleTypes.AnyAsync(b => b.Code == code);
    }
}
