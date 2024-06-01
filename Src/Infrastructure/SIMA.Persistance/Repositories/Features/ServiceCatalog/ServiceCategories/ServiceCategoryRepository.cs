using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceCategories.Contracts;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceCategories.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.ServiceCatalog.ServiceCategories;

public class ServiceCategoryRepository : Repository<ServiceCategory>, IServiceCategoryRepository
{
    private readonly SIMADBContext _context;

    public ServiceCategoryRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ServiceCategory> GetById(ServiceCategoryId Id)
    {
        var entity = await _context.ServiceCategories.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}