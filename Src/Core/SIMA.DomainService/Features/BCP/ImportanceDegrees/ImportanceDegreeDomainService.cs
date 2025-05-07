using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BCP.ImportanceDegrees.Contracts;
using SIMA.Domain.Models.Features.BCP.ImportanceDegrees.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.BCP.ImportanceDegrees;

public class ImportanceDegreeDomainService : IImportanceDegreeDomainService
{
    private readonly SIMADBContext _context;

    public ImportanceDegreeDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, ImportanceDegreeId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.ImportanceDegrees.AnyAsync(x => x.Code == code);
        else result = !await _context.ImportanceDegrees.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}