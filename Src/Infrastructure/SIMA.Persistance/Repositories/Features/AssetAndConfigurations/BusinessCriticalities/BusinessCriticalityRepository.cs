using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.BusinessCriticalities.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.BusinessCriticalities.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.AssetAndConfigurations.BusinessCriticalities;

public class BusinessCriticalityRepository : Repository<BusinessCriticality>, IBusinessCriticalityRepository
{
    private readonly SIMADBContext _context;

    public BusinessCriticalityRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<BusinessCriticality> GetById(BusinessCriticalityId Id)
    {
        var entity = await _context.BusinessCriticalities.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        
        
        return entity ?? throw SimaResultException.NotFound;
    }
}