using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BCP.Back_UpPeriods.Contracts;
using SIMA.Domain.Models.Features.BCP.Back_UpPeriods.Entities;
using SIMA.Domain.Models.Features.BCP.Back_UpPeriods.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.BCP.Back_UpPeriods;

public class BackupPeriodRepository : Repository<BackupPeriod>, IBackupPeriodRepository
{
    private readonly SIMADBContext _context;

    public BackupPeriodRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<BackupPeriod> GetById(BackupPeriodId Id)
    {
        var entity = await _context.BackupPeriods.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}