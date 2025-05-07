using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftTypes.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftTypes.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftTypes.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.TrustyDrafts.DraftTypes;

public class DraftTypeRepository : Repository<DraftType>, IDraftTypeRepository
{
    private readonly SIMADBContext _context;

    public DraftTypeRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<DraftType> GetById(DraftTypeId Id)
    {
        var entity = await _context.DraftTypes.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }

    public async Task<DraftType> GetByCode(string code)
    {
        var entity = await _context.DraftTypes.FirstOrDefaultAsync(x => x.Code == code);
        return entity;
    }
}