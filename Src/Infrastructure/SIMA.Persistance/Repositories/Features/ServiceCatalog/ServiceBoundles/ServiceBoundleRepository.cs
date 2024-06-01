using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceBoundles.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceBoundles.Interfaces;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.ServiceCatalog.ServiceBoundles;

public class ServiceBoundleRepository : Repository<ServiceBoundle>, IServiceBoundleRepository
{
    private readonly SIMADBContext _context;

    public ServiceBoundleRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ServiceBoundle> GetById(ServiceBoundleId Id)
    {
        var entity = await _context.ServiceBoundles.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}
