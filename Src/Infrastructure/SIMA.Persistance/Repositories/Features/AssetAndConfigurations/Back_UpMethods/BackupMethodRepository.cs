using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Back_Up_Methods.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Back_Up_Methods.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.AssetAndConfigurations.Back_UpMethods;

public class BackupMethodRepository : Repository<BackupMethod>, IBackupMethodRepository
{
    private readonly SIMADBContext _context;

    public BackupMethodRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<BackupMethod> GetById(BackupMethodId Id)
    {
        var entity = await _context.BackupMethods.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}