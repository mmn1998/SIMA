using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseTypes.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseTypes.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseTypes.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.AssetAndConfigurations.LicenseTypes;

public class LicenseTypeRepository : Repository<LicenseType>, ILicenseTypeRepository
{
    private readonly SIMADBContext _context;

    public LicenseTypeRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<LicenseType> GetById(LicenseTypeId Id)
    {
        var entity = await _context.LicenseTypes.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}