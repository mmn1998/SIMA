using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Back_Up_Methods.Contracts;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.AssetAndConfigurations.Back_Up_Methods;

public class BackupMethod : IBackupMethodDomainService
{
    private readonly SIMADBContext _context;

    public BackupMethod(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, BackupMethodId? id = null)
    {
        bool result = false;
        if (id == null) result = !await _context.BackupMethods.AnyAsync(x => x.Code == code);
        else result = !await _context.BackupMethods.AnyAsync(x => x.Code == code && x.Id != id);
        return result;
    }
}