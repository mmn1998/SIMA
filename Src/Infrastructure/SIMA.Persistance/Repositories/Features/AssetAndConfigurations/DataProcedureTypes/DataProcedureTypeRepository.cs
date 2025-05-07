using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedureTypes.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedureTypes.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedureTypes.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.AssetAndConfigurations.DataProcedureTypes;

public class DataProcedureTypeRepository : Repository<DataProcedureType>, IDataProcedureTypeRepository
{
    private readonly SIMADBContext _context;

    public DataProcedureTypeRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<DataProcedureType> GetById(DataProcedureTypeId Id)
    {
        var entity = await _context.DataProcedureTypes.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}