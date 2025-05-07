using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftStatuses.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftStatuses.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftStatuses.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.TrustyDrafts.DraftStatuses;

public class DraftStatusRepository : Repository<DraftStatus>, IDraftStatusRepository
{
    private readonly SIMADBContext _context;

    public DraftStatusRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<DraftStatus> GetById(DraftStatusId Id)
    {
        var entity = await _context.DraftStatuses.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
    public async Task<DraftStatus> GetByCode(string code)
    {
        var entity = await _context.DraftStatuses.FirstOrDefaultAsync(x => x.Code == code);
        return entity ?? throw SimaResultException.NotFound;
    }
}