using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemCustomFields.Contracts;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.AssetAndConfigurations.ConfigurationItemCustomFields
{
    public class ConfigurationItemCustomFieldDomainService : IConfigurationItemCustomFieldDomainService
    {
        private readonly SIMADBContext _context;

        public ConfigurationItemCustomFieldDomainService(SIMADBContext context)
        {
            _context = context;
        }
       
    }
}
