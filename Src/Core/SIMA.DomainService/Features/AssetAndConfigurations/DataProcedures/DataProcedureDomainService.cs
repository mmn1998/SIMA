using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.AssetAndConfigurations.DataProcedures;

public class DataProcedureDomainService : IDataProcedureDomainService
{
    private readonly SIMADBContext _context;

    public DataProcedureDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, DataProcedureId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.DataProcedures.AnyAsync(x => x.Code == code);
        else result = !await _context.DataProcedures.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}