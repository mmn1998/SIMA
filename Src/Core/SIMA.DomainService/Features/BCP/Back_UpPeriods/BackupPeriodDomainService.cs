using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BCP.Back_UpPeriods.Contracts;
using SIMA.Domain.Models.Features.BCP.Back_UpPeriods.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.BCP.Back_UpPeriods;

public class BackupPeriodDomainService : IBackupPeriodDomianService
{
    private readonly SIMADBContext _context;

    public BackupPeriodDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, BackupPeriodId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.BackupPeriods.AnyAsync(x => x.Code == code);
        else result = !await _context.BackupPeriods.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}