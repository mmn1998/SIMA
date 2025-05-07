using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.ConfigurationItemCustomFields;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.ConfigurationItemCustomFields
{
    public interface IConfigurationItemCustomFieldQueryRepository : IQueryRepository
    {
        Task<GetConfigurationItemCustomFieldQueryResult> GetById(GetConfigurationItemCustomFieldQuery request);
        Task<Result<IEnumerable<GetConfigurationItemCustomFieldQueryResult>>> GetAll(GetAllConfigurationItemCustomFieldsQuery request);
    }
}
