using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BCP.PlanTypes.Contracts;
using SIMA.Domain.Models.Features.BCP.PlanTypes.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.BCP.PlanTypes;

public class PlanTypeDomainService : IPlanTypeDomianService
{
    private readonly SIMADBContext _context;

    public PlanTypeDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, PlanTypeId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.PlanTypes.AnyAsync(x => x.Code == code);
        else result = !await _context.PlanTypes.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}