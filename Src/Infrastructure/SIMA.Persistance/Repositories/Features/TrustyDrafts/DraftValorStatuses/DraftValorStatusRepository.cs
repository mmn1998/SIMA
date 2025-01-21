using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftValorStatuses.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftValorStatuses.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftValorStatuses.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.TrustyDrafts.DraftValorStatuses;

public class DraftValorStatusRepository : Repository<DraftValorStatus>, IDraftValorStatusRepository
{
    private readonly SIMADBContext _context;

    public DraftValorStatusRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<DraftValorStatus> GetById(DraftValorStatusId Id)
    {
        var entity = await _context.DraftValorStatuses.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }

    public async Task<DraftValorStatus> GetByCode(string code)
    {
        var entity = await _context.DraftValorStatuses.FirstOrDefaultAsync(x => x.Code == code);
        return entity;
    }
}