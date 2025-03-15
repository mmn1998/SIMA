using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseTypes.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseTypes.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseTypes.ValueObjects;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.OperationalStatuses.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.OperationalStatuses.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.AssetAndConfigurations.OperationalStatuses;

public class OperationalStatuseRepository : Repository<OperationalStatus>, IOperationalStatusRepository
{
    private readonly SIMADBContext _context;

    public OperationalStatuseRepository(DbContext context, SIMADBContext context2) : base(context)
    {
        _context = context2;
    }

    public async Task<OperationalStatus> GetById(OperationalStatusId id)
    {
        var entity = await _context.OperationalStatuses.FirstOrDefaultAsync(x => x.Id == id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}