using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Interfaces;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.ServiceCatalog.CriticalActivities;

public class CriticalActivityRepository : Repository<CriticalActivity>, ICriticalActivityRepository
{
    private readonly SIMADBContext _context;

    public CriticalActivityRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<CriticalActivity> GetById(CriticalActivityId Id)
    {
        var entity = await _context.CriticalActivities
            .Include(x => x.CriticalActivityRisks)
            .Include(x => x.CriticalActivityAssets)
            .Include(x => x.CriticalActivityServices)
            .Include(x => x.CriticalActivityAssignStaffs)
            .Include(x => x.CriticalActivityExecutionPlans)
            .Include(x => x.CriticalActivityConfigurationItems)
            .FirstOrDefaultAsync(x => x.Id == Id)
            ;
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
    public async Task<CriticalActivity?> GetLastCriticalActivity()
    {
        var entity = await _context.CriticalActivities.OrderByDescending(x => x.CreatedAt).FirstOrDefaultAsync();
        return entity;
    }
}