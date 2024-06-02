using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceUserTypes.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceUserTypes.Interfaces;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.ServiceCatalog.ServiceUserTypes;

public class ServiceUserTypeRepository : Repository<ServiceUserType>, IServiceUserTypeRepository
{
    private readonly SIMADBContext _context;

    public ServiceUserTypeRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ServiceUserType> GetById(ServiceUserTypeId Id)
    {
        var entity = await _context.ServiceUserTypes.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}