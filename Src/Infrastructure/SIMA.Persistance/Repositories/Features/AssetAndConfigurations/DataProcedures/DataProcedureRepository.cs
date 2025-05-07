using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.AssetAndConfigurations.DataProcedures;

public class DataProcedureRepository : Repository<DataProcedure>, IDataProcedureRepository
{
    private readonly SIMADBContext _context;

    public DataProcedureRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<DataProcedure> GetById(DataProcedureId Id)
    {
        var entity = await _context.DataProcedures
            .Include(x => x.DataProcedureInputParam)
            .Include(x => x.DataProcedureOutputParam)
            .Include(x => x.DataProcedureDocuments)
            .Include(x => x.DataProcedureSupportTeams)
            .Include(x => x.ConfigurationItemDataProcedures)
            .FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}