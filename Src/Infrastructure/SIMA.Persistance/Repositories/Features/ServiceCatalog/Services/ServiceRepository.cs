using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Contracts;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.ServiceCatalog.Services;

public class ServiceRepository : Repository<Service>, IServiceRepository
{
    private readonly SIMADBContext _context;

    public ServiceRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Service> GetById(ServiceId Id)
    {
        _context.Database.SetCommandTimeout(180);
        var entity = await _context.Services
            .Include(x => x.ServiceCustomers)
            .Include(x => x.ServiceUsers)
            .Include(x => x.ServiceChanneles)
            .Include(x => x.PreRequisiteServicess)
            .Include(x => x.ServiceProviders)
            .Include(x => x.ServiceRisks)
            .Include(x => x.ServiceAssets)
            .Include(x => x.ServiceConfigurationItems)
            .Include(x => x.ServiceAssignStaffes)
            .Include(x => x.ServiceRelatedIssues)
            .Include(x => x.ServiceAvalibilities)
            .FirstOrDefaultAsync(x => x.Id == Id)
            ;
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }

    public async Task<Service?> GetLastService()
    {
        var entity = await _context.Services.OrderByDescending(x => x.CreatedAt).FirstOrDefaultAsync();
        return entity;
    }
}