using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedureTypes.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedureTypes.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.AssetAndConfigurations.DataProcedureTypes;

public class DataProcedureTypeDomainService : IDataProcedureTypeDomainService
{
    private readonly SIMADBContext _context;

    public DataProcedureTypeDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, DataProcedureTypeId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.DataProcedureTypes.AnyAsync(x => x.Code == code);
        else result = !await _context.DataProcedureTypes.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}