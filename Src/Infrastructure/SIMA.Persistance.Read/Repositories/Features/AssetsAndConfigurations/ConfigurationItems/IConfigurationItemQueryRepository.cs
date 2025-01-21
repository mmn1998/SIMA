using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.ConfigurationItems;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.ConfigurationItems;

public interface IConfigurationItemQueryRepository : IQueryRepository
{
    Task<Result<IEnumerable<GetConfigurationItemQueryResult>>> GetAll(GetAllConfigurationItemsQuery request);
    Task<Result<GetConfigurationItemQueryResult>> GetById(long id);
}
