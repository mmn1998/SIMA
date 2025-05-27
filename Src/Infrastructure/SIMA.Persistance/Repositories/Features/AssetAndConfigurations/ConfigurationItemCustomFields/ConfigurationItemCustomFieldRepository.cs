using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemCustomFields.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemCustomFields.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemCustomFields.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.AssetAndConfigurations.ConfigurationItemCustomFields
{
    public class ConfigurationItemCustomFieldRepository : Repository<ConfigurationItemCustomField>, IConfigurationItemCustomFieldRepository
    {
        private readonly SIMADBContext _context;

        public ConfigurationItemCustomFieldRepository(SIMADBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ConfigurationItemCustomField> GetById(ConfigurationItemCustomFieldId Id)
        {
            var entity = await _context.ConfigurationItemCustomFields.FirstOrDefaultAsync(x => x.Id == Id);
            entity.NullCheck();
            return entity ?? throw SimaResultException.NotFound;
        }
    }
}
