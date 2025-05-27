using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetCustomFields.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetCustomFields.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetCustomFields.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.AssetAndConfigurations.AssetCustomFields
{
    public class AssetCustomFieldRepository : Repository<AssetCustomField>, IAssetCustomFieldRepository
    {
        private readonly SIMADBContext _context;

        public AssetCustomFieldRepository(SIMADBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<AssetCustomField> GetById(AssetCustomFieldId Id)
        {
            var entity = await _context.AssetCustomFields.FirstOrDefaultAsync(x => x.Id == Id);
            entity.NullCheck();
            return entity ?? throw SimaResultException.NotFound;
        }
    }
}
