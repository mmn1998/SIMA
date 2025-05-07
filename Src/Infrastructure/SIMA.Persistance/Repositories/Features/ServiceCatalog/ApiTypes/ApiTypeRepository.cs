using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiTypes.Contracts;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiTypes.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiTypes.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.ServiceCatalog.ApiTypes;

public class ApiTypeRepository : Repository<ApiType>, IApiTypeRepository
{
    private readonly SIMADBContext _context;

    public ApiTypeRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ApiType> GetById(ApiTypeId Id)
    {
        var entity = await _context.ApiTypes.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}