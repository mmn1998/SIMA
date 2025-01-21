using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.ConfigurationItemStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.ConfigurationItemStatuses;

public interface IConfigurationItemStatusQueryRepository : IQueryRepository
{
    Task<GetConfigurationItemStatusQueryResult> GetById(GetConfigurationItemStatusQuery request);
    Task<Result<IEnumerable<GetConfigurationItemStatusQueryResult>>> GetAll(GetAllConfigurationItemStatusesQuery request);
}