using Microsoft.EntityFrameworkCore;
using SIMA.Application.Query.Contract.Features.Auths.ConfigurationAttributes;
using SIMA.Domain.Models.Features.Auths.ConfigurationAttributes.Entities;
using SIMA.Domain.Models.Features.Auths.ConfigurationAttributes.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.ConfigurationAttributes;

public class ConfigurationAttributeQueryRepository : IConfigurationAttributeQueryRepository
{
    private readonly SIMADBContext _context;

    public ConfigurationAttributeQueryRepository(SIMADBContext context)
    {
        _context = context;
    }

    public async Task<bool> CheckEnglishKeyIsExists(string key)
    {
        return await _context.ConfigurationAttributes.AnyAsync(x => x.EnglishKey.Equals(key, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<ConfigurationAttribute> FindById(long id)
    {
        var result = await _context.ConfigurationAttributes.FirstOrDefaultAsync(x => x.Id == new ConfigurationAttributeId(id));
        result.NullCheck();
        return result;
    }

    public async Task<List<GetConfigurationAttributeQueryResult>> GetAll()
    {
        return await _context.ConfigurationAttributes.AsNoTracking().Select(it => new GetConfigurationAttributeQueryResult
        {
            Id = it.Id.Value,
            ActiveStatusId = it.ActiveStatusId,
            EnglishKey = it.EnglishKey,
            IsUserConfige = it.IsUserConfige,
            Name = it.Name
        }).ToListAsync();
    }
}
