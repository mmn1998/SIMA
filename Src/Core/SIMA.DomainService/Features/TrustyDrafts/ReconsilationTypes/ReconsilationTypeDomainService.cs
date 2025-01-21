using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.TrustyDrafts.ReconsilationTypes.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.ReconsilationTypes.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.TrustyDrafts.ReconsilationTypes;

public class ReconsilationTypeDomainService : IReconsilationTypeDomainService
{
    private readonly SIMADBContext _context;

    public ReconsilationTypeDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, ReconsilationTypeId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.ReconsilationTypes.AnyAsync(x => x.Code == code);
        else result = !await _context.ReconsilationTypes.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}