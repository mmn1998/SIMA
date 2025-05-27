using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetCustomFields.Contracts;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.AssetAndConfigurations.AssetCustomFields
{
    public class AssetCustomFieldDomainService : IAssetCustomFieldDomainService
    {
        private readonly SIMADBContext _context;

        public AssetCustomFieldDomainService(SIMADBContext context)
        {
            _context = context;
        }
    }
}
