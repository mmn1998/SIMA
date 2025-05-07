using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseStatuses.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseStatuses.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseStatuses.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.AssetAndConfigurations.LicenseStatuses;

public class LicenseStatusRepository : Repository<LicenseStatus>, ILicenseStatusRepository
{
    private readonly SIMADBContext _context;

    public LicenseStatusRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<LicenseStatus> GetById(LicenseStatusId Id)
    {
        var entity = await _context.LicenseStatuses.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}