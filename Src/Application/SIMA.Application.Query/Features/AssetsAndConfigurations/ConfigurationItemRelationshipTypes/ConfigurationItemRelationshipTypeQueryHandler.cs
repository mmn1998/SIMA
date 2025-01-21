using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.ConfigurationItemRelationshipTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.ConfigurationItemRelationshipTypes;

namespace SIMA.Application.Query.Features.AssetsAndConfigurations.ConfigurationItemRelationshipTypes;

public class ConfigurationItemRelationshipTypeQueryHandler : IQueryHandler<GetConfigurationItemRelationshipTypeQuery, Result<GetConfigurationItemRelationshipTypeQueryResult>>,
    IQueryHandler<GetAllConfigurationItemRelationshipTypesQuery, Result<IEnumerable<GetConfigurationItemRelationshipTypeQueryResult>>>
{
    private readonly IConfigurationItemRelationshipTypeQueryRepository _repository;

    public ConfigurationItemRelationshipTypeQueryHandler(IConfigurationItemRelationshipTypeQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetConfigurationItemRelationshipTypeQueryResult>> Handle(GetConfigurationItemRelationshipTypeQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetConfigurationItemRelationshipTypeQueryResult>>> Handle(GetAllConfigurationItemRelationshipTypesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}