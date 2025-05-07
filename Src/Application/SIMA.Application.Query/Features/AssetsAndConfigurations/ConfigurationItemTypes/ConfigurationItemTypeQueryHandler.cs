using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.ConfigurationItemTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.ConfigurationItemTypes;

namespace SIMA.Application.Query.Features.AssetsAndConfigurations.ConfigurationItemTypes;

public class ConfigurationItemTypeQueryHandler : IQueryHandler<GetConfigurationItemTypeQuery, Result<GetConfigurationItemTypeQueryResult>>,
    IQueryHandler<GetAllConfigurationItemTypesQuery, Result<IEnumerable<GetConfigurationItemTypeQueryResult>>>
{
    private readonly IConfigurationItemTypeQueryRepository _repository;

    public ConfigurationItemTypeQueryHandler(IConfigurationItemTypeQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetConfigurationItemTypeQueryResult>> Handle(GetConfigurationItemTypeQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetConfigurationItemTypeQueryResult>>> Handle(GetAllConfigurationItemTypesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}