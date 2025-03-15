using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.ConfigurationItems;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.ConfigurationItems;

namespace SIMA.Application.Query.Features.AssetsAndConfigurations.ConfigurationItems;

public class ConfigurationItemQueryHandler :
    IQueryHandler<GetAllConfigurationItemsQuery, Result<IEnumerable<GetConfigurationItemQueryResult>>>,
    IQueryHandler<GetConfigurationItemQuery, Result<GetConfigurationItemQueryInfoResult>>,
    IQueryHandler<GetConfigurationItemByIdQuery, Result<GetConfigurationItemQueryInfoResult>>
{
    private readonly IConfigurationItemQueryRepository _repository;

    public ConfigurationItemQueryHandler(IConfigurationItemQueryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<GetConfigurationItemQueryResult>>> Handle(GetAllConfigurationItemsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }

    public async Task<Result<GetConfigurationItemQueryInfoResult>> Handle(GetConfigurationItemQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByCode(request);
    }

    public async Task<Result<GetConfigurationItemQueryInfoResult>> Handle(GetConfigurationItemByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request.Id);
        return Result.Ok(result);
    }
}
