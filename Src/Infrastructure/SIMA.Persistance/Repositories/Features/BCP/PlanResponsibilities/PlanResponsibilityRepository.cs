using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BCP.PlanResponsibilities.Contracts;
using SIMA.Domain.Models.Features.BCP.PlanResponsibilities.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.BCP.PlanResponsibilities;

public class PlanResponsibilityRepository : Repository<PlanResponsibility>, IPlanResponsibilityRepository
{
    private readonly SIMADBContext _context;

    public PlanResponsibilityRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<PlanResponsibility> GetById(PlanResponsibilityId Id)
    {
        var entity = await _context.PlanResponsibilities.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}