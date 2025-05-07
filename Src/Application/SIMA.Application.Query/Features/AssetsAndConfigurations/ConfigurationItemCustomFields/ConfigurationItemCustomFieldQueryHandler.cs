using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.ConfigurationItemCustomFields;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.ConfigurationItemCustomFields;

namespace SIMA.Application.Query.Features.AssetsAndConfigurations.ConfigurationItemCustomFields
{
    public class ConfigurationItemCustomFieldQueryHandler : IQueryHandler<GetConfigurationItemCustomFieldQuery, Result<GetConfigurationItemCustomFieldQueryResult>>,
    IQueryHandler<GetAllConfigurationItemCustomFieldsQuery, Result<IEnumerable<GetConfigurationItemCustomFieldQueryResult>>>
    {
        private readonly IConfigurationItemCustomFieldQueryRepository _repository;

        public ConfigurationItemCustomFieldQueryHandler(IConfigurationItemCustomFieldQueryRepository repository)
        {
            _repository = repository;
        }
        public async Task<Result<GetConfigurationItemCustomFieldQueryResult>> Handle(GetConfigurationItemCustomFieldQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetById(request);
            return Result.Ok(result);
        }

        public async Task<Result<IEnumerable<GetConfigurationItemCustomFieldQueryResult>>> Handle(GetAllConfigurationItemCustomFieldsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll(request);
        }
    }
}
