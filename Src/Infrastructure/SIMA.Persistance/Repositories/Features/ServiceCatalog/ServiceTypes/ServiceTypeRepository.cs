using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceTypes.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceTypes.Interfaces;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.ServiceCatalog.ServiceTypes;

public class ServiceTypeRepository : Repository<ServiceType>, IServiceTypeRepository
{
    private readonly SIMADBContext _context;

    public ServiceTypeRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ServiceType> GetById(ServiceTypeId Id)
    {
        var entity = await _context.ServiceTypes.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}