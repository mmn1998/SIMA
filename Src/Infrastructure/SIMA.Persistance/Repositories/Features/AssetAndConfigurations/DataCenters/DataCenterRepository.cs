using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataCenters.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataCenters.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataCenters.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.AssetAndConfigurations.DataCenters;

public class DataCenterRepository : Repository<DataCenter>, IDataCenterRepository
{
    private readonly SIMADBContext _context;

    public DataCenterRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<DataCenter> GetById(DataCenterId Id)
    {
        var entity = await _context.DataCenters.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}