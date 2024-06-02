using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceCustomerTypes.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceCustomerTypes.Interfaces;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.ServiceCatalog.ServiceCustomerTypes;

public class ServiceCustomerTypeRepository : Repository<ServiceCustomerType>, IServiceCustomerTypeRepository
{
    private readonly SIMADBContext _context;

    public ServiceCustomerTypeRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ServiceCustomerType> GetById(ServiceCustomerTypeId Id)
    {
        var entity = await _context.ServiceCustomerTypes.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}