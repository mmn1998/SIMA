using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.ConfigurationItemTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.ConfigurationItemTypes;

public interface IConfigurationItemTypeQueryRepository : IQueryRepository
{
    Task<GetConfigurationItemTypeQueryResult> GetById(GetConfigurationItemTypeQuery request);
    Task<Result<IEnumerable<GetConfigurationItemTypeQueryResult>>> GetAll(GetAllConfigurationItemTypesQuery request);
}