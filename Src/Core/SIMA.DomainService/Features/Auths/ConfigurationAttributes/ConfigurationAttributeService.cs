using SIMA.Domain.Models.Features.Auths.ConfigurationAttributes;
using SIMA.Persistance.Read.Repositories.Features.Auths.ConfigurationAttributes;

namespace SIMA.DomainService.Features.Auths.ConfigurationAttributes
{
    public class ConfigurationAttributeService : IConfigurationAttributeService
    {
        private readonly IConfigurationAttributeQueryRepository _repository;

        public ConfigurationAttributeService(IConfigurationAttributeQueryRepository repository)
        {
            _repository = repository;
        }
        public async Task<bool> CheckEnglishKey(string key)
        {
            return await _repository.CheckEnglishKeyIsExists(key);
        }
    }
}
