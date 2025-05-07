using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BCP.PlanTypes.Contracts;
using SIMA.Domain.Models.Features.BCP.PlanTypes.Entities;
using SIMA.Domain.Models.Features.BCP.PlanTypes.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.BCP.PlanTypes;

public class PlanTypeRepository : Repository<PlanType>, IPlanTypeRepository
{
    private readonly SIMADBContext _context;

    public PlanTypeRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<PlanType> GetById(PlanTypeId Id)
    {
        var entity = await _context.PlanTypes.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}