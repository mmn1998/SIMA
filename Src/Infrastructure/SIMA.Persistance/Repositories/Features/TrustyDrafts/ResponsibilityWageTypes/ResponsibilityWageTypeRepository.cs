using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.TrustyDrafts.ResponsibilityWageTypes.Contrcts;
using SIMA.Domain.Models.Features.TrustyDrafts.ResponsibilityWageTypes.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.ResponsibilityWageTypes.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.TrustyDrafts.ResponsibilityWageTypes;

public class ResponsibilityWageTypeRepository : Repository<ResponsibilityWageType>, IResponsibilityWageTypeRepository
{
    private readonly SIMADBContext _context;

    public ResponsibilityWageTypeRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ResponsibilityWageType> GetById(ResponsibilityWageTypeId Id)
    {
        var entity = await _context.ResponsibilityWageTypes.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }

    public async Task<ResponsibilityWageType> GetByCode(string code)
    {
        var entity = await _context.ResponsibilityWageTypes.FirstOrDefaultAsync(x => x.Code == code);
        return entity;
    }
}