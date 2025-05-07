using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.MatrixAs.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.MatrixAs.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.RiskManagers.MatrixAs;

public class MatrixADomainService : IMatrixADomainService
{
    private readonly SIMADBContext _context;

    public MatrixADomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, MatrixAId? id = null)
    {
        bool result = false;
        if (id == null) result = !await _context.MatrixAs.AnyAsync(x => x.Code == code);
        else result = !await _context.MatrixAs.AnyAsync(x => x.Code == code && x.Id != id);
        return result;
    }
}