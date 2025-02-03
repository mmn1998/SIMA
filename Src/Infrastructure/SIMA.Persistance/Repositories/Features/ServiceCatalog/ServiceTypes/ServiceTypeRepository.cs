using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceTypes.Contracts;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceTypes.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceTypes.ValueObjects;
using SIMA.Framework.Common.Exceptions;
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
        return await _context.ServiceTypes.FirstOrDefaultAsync(x => x.Id == Id) ?? throw SimaResultException.NotFound;
    }
}
