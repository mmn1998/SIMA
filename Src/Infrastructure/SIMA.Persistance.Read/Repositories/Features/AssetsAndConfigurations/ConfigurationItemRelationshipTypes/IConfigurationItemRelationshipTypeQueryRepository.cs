using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.ConfigurationItemRelationshipTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.ConfigurationItemRelationshipTypes;

public interface IConfigurationItemRelationshipTypeQueryRepository : IQueryRepository
{
    Task<GetConfigurationItemRelationshipTypeQueryResult> GetById(GetConfigurationItemRelationshipTypeQuery request);
    Task<Result<IEnumerable<GetConfigurationItemRelationshipTypeQueryResult>>> GetAll(GetAllConfigurationItemRelationshipTypesQuery request);
}